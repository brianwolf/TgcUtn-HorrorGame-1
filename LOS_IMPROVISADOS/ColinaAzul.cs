﻿using Microsoft.DirectX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TgcViewer.Utils;
using TgcViewer.Utils.TgcGeometry;
using TgcViewer.Utils.TgcSceneLoader;

namespace AlumnoEjemplos.LOS_IMPROVISADOS
{
    class ColinaAzul
    {
        #region Singleton
        private static volatile ColinaAzul instancia = null;

        public static ColinaAzul Instance
        {
            get
            { return newInstance(); }
        }

        internal static ColinaAzul newInstance()
        {
            if (instancia != null) { }
            else
            {
                instancia = new ColinaAzul();
            }
            return instancia;
        }

        #endregion

        Dictionary<string,TgcBoundingBox> bloquesCuartos;

        private ColinaAzul()
        {
            bloquesCuartos = new Dictionary<string, TgcBoundingBox>();
        }

        public void calcularBoundingBoxes(Dictionary<string,List<TgcMesh>> cuartos,int cantidadCuartos)
        {
            for (int i = 1; i <= cantidadCuartos; i++)
            {
                List<TgcMesh> cuarto = null;
                cuartos.TryGetValue("r_" + i, out cuarto);

                if (cuarto==null)
                {
                    bloquesCuartos.Add("r_"+i,calcularBoundingBox(cuarto));
                }
            }
        }

        private TgcBoundingBox calcularBoundingBox(List<TgcMesh> cuarto)
        {
            Vector3 pMin = cuarto[0].BoundingBox.PMin;
            Vector3 pMax = cuarto[0].BoundingBox.PMax;

            foreach (TgcMesh mesh in cuarto)
            {
                pMin = menoresCoordenadasDe(pMin, mesh.BoundingBox.PMin);

                pMax = mayoresCoordenadasDe(pMax, mesh.BoundingBox.PMax);
            }

            return new TgcBoundingBox(pMin, pMax);
        }

        private Vector3 menoresCoordenadasDe(Vector3 point, Vector3 otherPoint)
        {
            return new Vector3(FastMath.Min(point.X, otherPoint.X),
            FastMath.Min(point.Y, otherPoint.Y),
            FastMath.Min(point.Z, otherPoint.Z));
        }

        private Vector3 mayoresCoordenadasDe(Vector3 point, Vector3 otherPoint)
        {
            return new Vector3(FastMath.Max(point.X, otherPoint.X),
            FastMath.Max(point.Y, otherPoint.Y),
            FastMath.Max(point.Z, otherPoint.Z));
        }
    }
}