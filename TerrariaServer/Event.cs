﻿using System;
namespace Terraria
{
    public class Event
    {
        private bool state = true;
        public void setState(bool a)
        {
            this.state = a;
        }
        public bool getState()
        {
            return this.state;
        }
    }
}
