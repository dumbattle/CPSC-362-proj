using System.Collections;

namespace AustinsExamples.GameState {
    // don't want to type out full namespace every time
    public abstract class UIManager : AustinsExamples.UI.UIManager { }


    public abstract class GameStateUIManager : UIManager {
        static GameStateUIManager _main;

        // base input data  still exists
        // color - still here
        // tile - still here

        // additional input data
        public static bool playRecieved { get; protected set; }
        public static bool pausedRecieved { get; protected set; }
        public static bool click { get; protected set; }

        protected virtual void Awake() {
            _main = this;
        }

        // updated clear
        protected override void Clear() {
            base.Clear();

            SelfClear();
        }

        // don't clear base signals
        // usefull if we want to use an existing Base UIManager
        protected virtual void SelfClear() {
            playRecieved = false;
            pausedRecieved = false;
            click = false;
        }



        // Not all UI will be active at once
        // GameControllers will signal what input it is expecting
        public static void SetPlayState() {
            _main.ShowPlayUI();
        }
        public static void SetPauseState() {
            _main.ShowPausedUI();
        }

        // static class does not have direct access to UI objects
        // so it will call these methods instead
        protected abstract void ShowPlayUI();
        protected abstract void ShowPausedUI();



        // this 'hides' the base register class. That is what the 'new' indicates
        // if you want to use the base Register, do:
        // => UIManager.Register.Color(c)
        protected new static partial class Register {
            public static void Play() {
                playRecieved = true;
            }
            public static void Pause() {
                pausedRecieved = true;
            }
            public static void Click() {
                click = true;
            }
        }
    }
}