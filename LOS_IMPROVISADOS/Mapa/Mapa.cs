﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TgcViewer;
using TgcViewer.Utils.TgcGeometry;
using TgcViewer.Utils.TgcSceneLoader;

namespace AlumnoEjemplos.LOS_IMPROVISADOS
{
    class Mapa
    {

        #region Singleton
        private static volatile Mapa instancia = null;

        public static Mapa Instance
        {
            get
            { return newInstance(); }
        }

        internal static Mapa newInstance()
        {
            if (instancia != null) { }
            else
            {
               new Mapa();
            }
            return instancia;
        }

        #endregion

        public TgcScene escena { get; set; }

        public Mapa()
        {
            TgcSceneLoader loader = new TgcSceneLoader();
            escena = loader.loadSceneFromFile(
                GuiController.Instance.AlumnoEjemplosDir + "Media\\mapa\\mapasm-TgcScene.xml",
                GuiController.Instance.AlumnoEjemplosDir + "Media\\mapa\\");

                instancia = this;
        }


        internal void dispose()
        {
            escena.disposeAll();
        }

        public bool colisionaEsfera(TgcBoundingSphere esfera,ref TgcBoundingBox obstaculo)
        {

            foreach (TgcMesh mesh in escena.Meshes)
            {
                if (TgcCollisionUtils.testSphereAABB(esfera,mesh.BoundingBox))
                {
                    obstaculo = mesh.BoundingBox;

                    return true;
                }
            }
            return false;
        }
        public bool colisionaEsfera(TgcBoundingSphere esfera)
        {
            TgcBoundingBox b = new TgcBoundingBox();
            return colisionaEsfera(esfera, ref b);
        }

    }
}
