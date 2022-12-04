﻿using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace FlatUI
{
    [DefaultEvent( "CheckedChanged" )]
    public class FlatToggle : Control
    {
        private int W;
        private int H;
        private MouseState State { get; set; }

        public event CheckedChangedEventHandler CheckedChanged;

        public delegate void CheckedChangedEventHandler( object sender );

        [Flags()]
        public enum _Options
        {
            Style1,
            Style2,
            Style3
        }

        [Category( "Options" )]
        public _Options Options { get; set; }

        [Category( "Options" )]
        public bool Checked { get; set; } = false;

        protected override void OnTextChanged( EventArgs e )
        {
            base.OnTextChanged( e );
            Invalidate();
        }

        protected override void OnResize( EventArgs e )
        {
            base.OnResize( e );
            Width = 76;
            Height = 33;
        }

        protected override void OnMouseEnter( EventArgs e )
        {
            base.OnMouseEnter( e );
            State = MouseState.Over;
            Invalidate();
        }

        protected override void OnMouseDown( MouseEventArgs e )
        {
            base.OnMouseDown( e );
            State = MouseState.Down;
            Invalidate();
        }

        protected override void OnMouseLeave( EventArgs e )
        {
            base.OnMouseLeave( e );
            State = MouseState.None;
            Invalidate();
        }

        protected override void OnMouseUp( MouseEventArgs e )
        {
            base.OnMouseUp( e );
            State = MouseState.Over;
            Invalidate();
        }

        protected override void OnClick( EventArgs e )
        {
            base.OnClick( e );
            Checked = !Checked;
            CheckedChanged?.Invoke( this );
        }

        private Color BaseColor = Helpers.FlatColor;
        private readonly Color BaseColorRed = Color.FromArgb( 220, 85, 96 ); //Red
        private readonly Color BGColor = Color.FromArgb( 233, 95, 98 ); //Red
        private readonly Color ToggleColor = Color.FromArgb( 24, 22, 43 ); //Dark
        private readonly Color TextColor = Helpers.FlatWhite;

        public FlatToggle()
        {
            SetStyle(
                ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw |
                ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor, true );
            DoubleBuffered = true;
            BackColor = Color.Transparent;
            Size = new Size( 44, Height + 1 );
            Cursor = Cursors.Hand;
            Font = new Font( "Tahoma", 10 );
            Size = new Size( 76, 33 );
        }

        protected override void OnPaint( PaintEventArgs e )
        {
            UpdateColors();

            var B = new Bitmap( Width, Height );
            var G = Graphics.FromImage( B );
            W = Width - 1;
            H = Height - 1;

            var GP = new GraphicsPath();
            var GP2 = new GraphicsPath();
            var Base = new Rectangle( 0, 0, W, H );
            var Toggle = new Rectangle( Convert.ToInt32( W / 2 ), 0, 39, H );

            var _with9 = G;
            _with9.SmoothingMode = SmoothingMode.HighQuality;
            _with9.PixelOffsetMode = PixelOffsetMode.HighQuality;
            _with9.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            _with9.Clear( BackColor );

            switch ( Options )
            {
                case _Options.Style1:
                    //-- Style 1
                    //-- Base
                    GP = Helpers.RoundRec( Base, 7 );
                    GP2 = Helpers.RoundRec( Toggle, 6 );
                    _with9.FillPath( new SolidBrush( BGColor ), GP );
                    _with9.FillPath( new SolidBrush( ToggleColor ), GP2 );

                    //-- Text
                    _with9.DrawString( "OFF", Font, new SolidBrush( BGColor ), new Rectangle( 19, 1, W, H ),
                        Helpers.CenterSF );

                    if ( Checked )
                    {
                        //-- Base
                        GP = Helpers.RoundRec( new Rectangle( -1, 0, W, H ), 6 );
                        GP2 = Helpers.RoundRec( new Rectangle( Convert.ToInt32( W / 2 ), 0, 39, H + 1 ), 6 );
                        _with9.FillPath( new SolidBrush( ToggleColor ), GP );
                        _with9.FillPath( new SolidBrush( BaseColor ), GP2 );

                        //-- Text
                        _with9.DrawString( "ON", Font, new SolidBrush( BaseColor ), new Rectangle( 8, 7, W, H ),
                            Helpers.NearSF );
                    }

                    break;

                case _Options.Style2:
                    //-- Style 2
                    //-- Base
                    GP = Helpers.RoundRec( Base, 6 );
                    Toggle = new Rectangle( 4, 4, 36, H - 8 );
                    GP2 = Helpers.RoundRec( Toggle, 4 );
                    _with9.FillPath( new SolidBrush( BaseColorRed ), GP );
                    _with9.FillPath( new SolidBrush( ToggleColor ), GP2 );

                    //-- Lines
                    _with9.DrawLine( new Pen( TextColor ), 18, 20, 18, 12 );
                    _with9.DrawLine( new Pen( TextColor ), 22, 20, 22, 12 );
                    _with9.DrawLine( new Pen( TextColor ), 26, 20, 26, 12 );

                    //-- Text
                    _with9.DrawString( "r", new Font( "Marlett", 8 ), new SolidBrush( TextColor ),
                        new Rectangle( 19, 2, Width, Height ), Helpers.CenterSF );

                    if ( Checked )
                    {
                        GP = Helpers.RoundRec( Base, 6 );
                        Toggle = new Rectangle( Convert.ToInt32( W / 2 ) - 2, 4, 36, H - 8 );
                        GP2 = Helpers.RoundRec( Toggle, 4 );
                        _with9.FillPath( new SolidBrush( BaseColor ), GP );
                        _with9.FillPath( new SolidBrush( ToggleColor ), GP2 );

                        //-- Lines
                        _with9.DrawLine( new Pen( BGColor ), Convert.ToInt32( W / 2 ) + 12, 20,
                            Convert.ToInt32( W / 2 ) + 12, 12 );
                        _with9.DrawLine( new Pen( BGColor ), Convert.ToInt32( W / 2 ) + 16, 20,
                            Convert.ToInt32( W / 2 ) + 16, 12 );
                        _with9.DrawLine( new Pen( BGColor ), Convert.ToInt32( W / 2 ) + 20, 20,
                            Convert.ToInt32( W / 2 ) + 20, 12 );

                        //-- Text
                        _with9.DrawString( "ü", new Font( "Wingdings", 14 ), new SolidBrush( TextColor ),
                            new Rectangle( 8, 7, Width, Height ), Helpers.NearSF );
                    }

                    break;

                case _Options.Style3:
                    //-- Style 3
                    //-- Base
                    GP = Helpers.RoundRec( Base, 16 );
                    Toggle = new Rectangle( W - 28, 4, 22, H - 8 );
                    GP2.AddEllipse( Toggle );
                    _with9.FillPath( new SolidBrush( ToggleColor ), GP );
                    _with9.FillPath( new SolidBrush( BaseColorRed ), GP2 );

                    //-- Text
                    _with9.DrawString( "OFF", Font, new SolidBrush( BaseColorRed ), new Rectangle( -12, 2, W, H ),
                        Helpers.CenterSF );

                    if ( Checked )
                    {
                        //-- Base
                        GP = Helpers.RoundRec( Base, 16 );
                        Toggle = new Rectangle( 6, 4, 22, H - 8 );
                        GP2.Reset();
                        GP2.AddEllipse( Toggle );
                        _with9.FillPath( new SolidBrush( ToggleColor ), GP );
                        _with9.FillPath( new SolidBrush( BaseColor ), GP2 );

                        //-- Text
                        _with9.DrawString( "ON", Font, new SolidBrush( BaseColor ), new Rectangle( 12, 2, W, H ),
                            Helpers.CenterSF );
                    }

                    break;
            }

            base.OnPaint( e );
            G.Dispose();
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            e.Graphics.DrawImageUnscaled( B, 0, 0 );
            B.Dispose();
        }

        private void UpdateColors()
        {
            var colors = Helpers.GetColors( this );

            BaseColor = colors.Flat;
        }
    }
}