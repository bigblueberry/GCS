﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Grid.Framework;
using Grid.Framework.GUIs;

using Button = Grid.Framework.GUIs.Button;
using MenuStrip = System.Windows.Forms.MenuStrip;
using Color = Microsoft.Xna.Framework.Color;

namespace GCS
{
    public class Main : Scene
    {
        private ConstructComponent _construct;
        protected override void Initialize()
        {
            base.Initialize();
            // 윈도우 리사이즈는 모노게임자체에 버그가 있다고 함
            /*
            Window.ClientSizeChanged += (s, e) =>
            {
                _windowResized = !_windowResized;
                if (_windowResized)
                {
                    _graphics.PreferredBackBufferWidth = Window.ClientBounds.Width;
                    _graphics.PreferredBackBufferHeight = Window.ClientBounds.Height;
                    _graphics.ApplyChanges();
                }
            };
            Window.AllowUserResizing = true;
            */
        }
        protected override void InitSize()
        {
            base.InitSize();
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            //BackColor = new Color(133, 182, 203);
            BackColor = new Color(221, 255, 221);
            BackColor = Color.White;
            Debugger.Color = Color.Black;
            GraphicsDevice.BlendState = BlendState.Opaque;
            GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            GraphicsDevice.RasterizerState = RasterizerState.CullCounterClockwise;
            GraphicsDevice.SamplerStates[0] = SamplerState.LinearWrap;
            _graphics.PreferMultiSampling = true;
            GameObject con = new GameObject("construct");
            Instantiate(con);
            _construct = con.AddComponent<ConstructComponent>();
            _construct.Enabled = true;

            Resources.LoadAll();
            InitGUI();
            MainCamera.AddComponent<Grid.Framework.Components.Movable2DCamera>();

            //GameObject test = new GameObject("test");
            //test.AddComponent<GeometryTestComponent>();
            //test.Enabled = false;
            //Instantiate(test);

        }
        
        private void InitGUI()
        {
            MenuStrip shapeStrip = new MenuStrip()
            {
                BackColor = System.Drawing.Color.White,
                Dock = DockStyle.Left
            };

            AddStripItem(shapeStrip, @"D:\Image\design\GCS\circle.png").Click += (b, d) => _construct.ChangeState(DrawState.CIRCLE);
            AddStripItem(shapeStrip, @"D:\Image\design\GCS\segment.png").Click += (b, d) => _construct.ChangeState(DrawState.SEGMENT);
            AddStripItem(shapeStrip, @"D:\Image\design\GCS\line.png").Click += (b, d) => _construct.ChangeState(DrawState.LINE);
            AddStripItem(shapeStrip, @"D:\Image\design\GCS\vector.png").Click += (b, d) => _construct.ChangeState(DrawState.VECTOR);
            AddStripItem(shapeStrip, @"D:\Image\design\GCS\dot.png").Click += (b, d) => _construct.ChangeState(DrawState.DOT);

            VScrollBar vscroll = new VScrollBar();
            vscroll.Dock = DockStyle.Right;
            HScrollBar hscroll = new HScrollBar();
            hscroll.Dock = DockStyle.Bottom;

            var construct = GameObject.Find("construct");

            var move = construct.AddComponent<MoveConstructComponent>();
            move.Comp = construct.GetComponent<ConstructComponent>();
            move.Hscroll = hscroll;
            move.Vscroll = vscroll;

            var menu = new MenuStrip()
            {
                BackColor = System.Drawing.Color.White
            };

            var con = new ToolStripMenuItem("작도(&C)");
            con.DropDownItems.Add("평행선");
            con.DropDownItems[0].Click += (s, e) => _construct.SelectConstruct(ConstructType.ParallelLine);
            
            menu.Items.Add(con);

            var control = Control.FromHandle(Window.Handle);
            control.Controls.Add(shapeStrip);
            control.Controls.Add(menu);
            control.Controls.Add(vscroll);
            control.Controls.Add(hscroll);
        }

        private ToolStripMenuItem AddStripItem(MenuStrip strip, string imgPath)
        {
            var imageSize = new Size(60, 60);
            var item = new ToolStripMenuItem(new Bitmap(Image.FromFile(imgPath), imageSize));
            item.ImageAlign = ContentAlignment.MiddleCenter;
            item.ImageScaling = ToolStripItemImageScaling.None;
            item.AutoSize = true;
            strip.Items.Add(item);
            return item;
        }
    }
}
