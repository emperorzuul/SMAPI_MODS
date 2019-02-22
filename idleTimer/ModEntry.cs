using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;

namespace IdleTimer
{
    /// <summary>The mod entry point.</summary>
    public class ModEntry : Mod
    {

        private bool _isIdle;
        private SButton _lastPressed;
        private int _lastPressedTime;
        int _idleTimer;
        
        
        /*********
        ** Public methods
        *********/
        public override void Entry(IModHelper helper)
        {
            _idleTimer = 300;
            helper.Events.Input.ButtonPressed += InputEvents_ButtonPressed;
            helper.Events.GameLoop.UpdateTicked += GameEvents_UpdateTick;
            helper.Events.GameLoop.TimeChanged += TimeOfDayChanged;
        }

        private void TimeOfDayChanged(object sender, TimeChangedEventArgs e)
        {

            if (_isIdle)
            {
                Game1.pauseThenMessage(200, "You are now idle", false);
                Monitor.Log($"{Game1.player.Name} idle at {Game1.timeOfDay}");
            }
            if (Game1.timeOfDay > _lastPressedTime + _idleTimer)
                _isIdle = true;
        }


        /*********
        ** Private methods
        *********/
        /// <summary>The method invoked when the player presses a controller, keyboard, or mouse button.</summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event data.</param>
        private void InputEvents_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            // ignore if player hasn't loaded a save yet
            if (!Context.IsWorldReady)
                return;
            _isIdle = false;
            _lastPressedTime = Game1.timeOfDay;
        }

        /*********
        ** Private methods
        *********/
        /// <summary>The method invoked after the game updates (roughly 60 times per second).</summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        /// <remarks>
        /// All times are shown in the game's internal format, which is essentially military time with support for
        /// times past midnight (e.g. 2400 is midnight, 2600 is 2am).
        private void GameEvents_UpdateTick(object sender, UpdateTickedEventArgs e)
        {
            if (!Context.IsWorldReady)
                return;

        }
    }
}