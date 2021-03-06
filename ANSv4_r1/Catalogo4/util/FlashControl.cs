﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Catalogo.varios
{
    public partial class FlashControl : UserControl
    {
        public FlashControl()
        {
            InitializeComponent();
        }

        public string file
        {
            get
            {
                return axShockwaveFlash1.Movie;
            }
            set
            {
                axShockwaveFlash1.Movie = value;
            }
        }

        internal void play()
        {
            try
            {
                if (axShockwaveFlash1.Movie.Length != 0)
                {
                    axShockwaveFlash1.Loop = true;
                    axShockwaveFlash1.LoadMovie(0, file);

                    //if (file.IndexOf("banner") <= 0)
                    //{
                    //SwfParser swfParser = new SwfParser();
                    //Rectangle rectangle = swfParser.GetDimensions(file);
                    //axShockwaveFlash1.Width = rectangle.Width;
                    //axShockwaveFlash1.Height = rectangle.Height;
                    //}

                    axShockwaveFlash1.Play();
                }
            }
            catch (Exception ex)
            {
                util.errorHandling.ErrorLogger.LogMessage(ex);
                
                throw ex;
            }
        }


        internal void stop()
        {
            if (axShockwaveFlash1.IsPlaying())
            {
                axShockwaveFlash1.StopPlay();
            }
        }
    }
}
