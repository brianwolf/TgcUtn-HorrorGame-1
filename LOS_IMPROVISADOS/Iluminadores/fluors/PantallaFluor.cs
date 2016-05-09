﻿using AlumnoEjemplos.LOS_IMPROVISADOS.Iluminadores.IyCA;
using Microsoft.DirectX;
using System.Drawing;
using TgcViewer;
using TgcViewer.Utils._2D;
using TgcViewer.Utils.TgcSceneLoader;

namespace AlumnoEjemplos.LOS_IMPROVISADOS.Iluminadores.fluors
{
    class PantallaFluor : APantalla
    {
        public PantallaFluor()
        {
            sprite = new TgcSprite();
            sprite.Texture = TgcTexture.createTexture(GuiController.Instance.AlumnoEjemplosDir + "Media\\Texturas\\fluor.png");
        }

        public override void init()
        {
            Size screenSize = GuiController.Instance.Panel3d.Size;
            sprite.Position = new Vector2(screenSize.Width - (screenSize.Width / 4), 0.40f * screenSize.Height);
            sprite.Scaling = new Vector2((float)0.0003 * screenSize.Width, (float)0.0006 * screenSize.Height);
        }

        public override void render()
        {
            GuiController.Instance.Drawer2D.beginDrawSprite();
            sprite.render();
            GuiController.Instance.Drawer2D.endDrawSprite();
        }
    }
}