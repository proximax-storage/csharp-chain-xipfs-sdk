﻿using System;
using System.Collections.Generic;
using System.Text;

namespace IO.Proximax.SDK.Async
{
    public class AsyncTask
    {
        private bool Done { get; set; }
        private bool Cancelled { get; set; }

        public AsyncTask()
        {
            Done = false;
            Cancelled = false;
        }

        public void SetToDone()
        {
            if (!Cancelled)
                Done = true;
        }
        
        public void SetToCancelled()
        {
            if (!Done)
                Cancelled = true;
        }
        
    }
}
