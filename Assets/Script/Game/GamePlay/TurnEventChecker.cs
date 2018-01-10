using UnityEngine;
using System.Collections.Generic;

namespace Assets.Script.Game.GamePlay
{
    class TurnEventChecker : MonoBehaviour
    {
        private IList<string> openEvents;
        private IList<string> closeEvents;
        private IList<string> curEvents;
        private IDictionary<string, IList<string>> openEventsForClose;

        delegate void EventHandler();
        private Dictionary<string, EventHandler> eventHandlers;
        private Dispatcher dispatcher;

        private void Start()
        {
            openEvents = new List<string>();
            closeEvents = new List<string>();
            curEvents = new List<string>();
            openEventsForClose = new Dictionary<string, IList<string>>();
            InitTurnEventChecker();
            dispatcher = GetComponent<Dispatcher>();
            InitEventHandlers();
        }

        private void InitTurnEventChecker()
        {
            openEvents.Add("CometLaunch");
            openEvents.Add("SupernovaDeath");
            openEvents.Add("BlackholeBirth");

            closeEvents.Add("SizeExtensionEnd");
            closeEvents.Add("BlackholeDeath");
            closeEvents.Add("SingleBurstWaveDestroy");
            closeEvents.Add("EndSession");

            MarkCloseEventForOpen("CometLaunch", "SizeExtensionEnd");
            MarkCloseEventForOpen("CometLaunch", "EndSession");
            MarkCloseEventForOpen("SupernovaDeath", "SingleBurstWaveDestroy");
            MarkCloseEventForOpen("BlackholeBirth", "BlackholeDeath");
        }

        private void InitEventHandlers()
        {
            eventHandlers = new Dictionary<string, EventHandler>();
            eventHandlers["CometLaunch"] = ActivateChecking;
            eventHandlers["SupernovaDeath"] = () => { };
            eventHandlers["BlackholeBirth"] = () => { };
            eventHandlers["SizeExtensionEnd"] = () => { };
            eventHandlers["BlackholeDeath"] = () => { };
            eventHandlers["BurstWaveDeath"] = () => { };
        }

        private void ActivateChecking()
        {
            ApplyEvent("CometLaunch");
            eventHandlers["SupernovaDeath"] = () => { ApplyEvent("SupernovaDeath"); };
            eventHandlers["BlackholeBirth"] = () => { ApplyEvent("BlackholeBirth"); };
            eventHandlers["SizeExtensionEnd"] = () => { ApplyEvent("SizeExtensionEnd"); };
            eventHandlers["BlackholeDeath"] = () => { ApplyEvent("BlackholeDeath"); };
            eventHandlers["SingleBurstWaveDestroy"] = () => { ApplyEvent("SingleBurstWaveDestroy"); };
            eventHandlers["EndSession"] = () => { ApplyEvent("EndSession"); };
        }

        private void DeactivateChecking()
        {
            eventHandlers["SupernovaDeath"] = () => { };
            eventHandlers["BlackholeBirth"] = () => { };
            eventHandlers["SizeExtensionEnd"] = () => { };
            eventHandlers["BlackholeDeath"] = () => { };
            eventHandlers["SingleBurstWaveDestroy"] = () => { };
            eventHandlers["EndSession"] = () => { };
        }

        private void Update()
        {
            if (!dispatcher.IsEmpty()) {
                string evtName = dispatcher.ReceiveEvent().Name; 
                eventHandlers[evtName].Invoke();
            }
        }

        private void MarkCloseEventForOpen(string openEvent, string closeEvent)
        {
            bool isExsists = openEventsForClose.ContainsKey(closeEvent);
            if (!isExsists) {
                openEventsForClose[closeEvent] = new List<string>();
            }
            openEventsForClose[closeEvent].Add(openEvent);
        }

        public void ApplyEvent(string evt)
        {
            bool isOpenEvt = openEvents.Contains(evt);
            if (isOpenEvt) {
                ApplyOpenEvent(evt);

                string evts = "";
                foreach (var e in curEvents) {
                    evts += e + " ";
                }
                Debug.Log("Events: " + evts);
                return;
            }
            bool isCloseEvt = closeEvents.Contains(evt);
            if (isCloseEvt) {
                ApplyCloseEvent(evt);

                string evts = "";
                foreach (var e in curEvents) {
                    evts += e;
                }
                Debug.Log("Events: " + evts);
                return;
            }
        }

        private void ApplyOpenEvent(string evt)
        {
            curEvents.Add(evt);
        }

        private void ApplyCloseEvent(string evt)
        {
            IList<string> possibleOpenEvents = openEventsForClose[evt];
            foreach (string openEvt in possibleOpenEvents) {
                if (curEvents.Contains(openEvt)) {
                    curEvents.Remove(openEvt);
                    break;
                }
            }
            if (curEvents.Count == 0) {
                FindObjectOfType<EventBus>().TriggerEvent(new Event("EventCheckerEmpty"));
                DeactivateChecking();
            }
        }
    }
}
