﻿using AlumnoEjemplos.LOS_IMPROVISADOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.DirectX;
using AlumnoEjemplos.LOS_IMPROVISADOS.Personajes.Boss;

namespace AlumnoEjemplos.LOS_IMPROVISADOS
{
    class SeguirPersonaje : Comportamiento
    {
        private DiosMapa diosMapa;

        private Vector3 posAnterior;

        public SeguirPersonaje()
        {
            diosMapa = DiosMapa.Instance;
        }
        public Vector3 proximoPunto(Vector3 posicionActual)
        {
            Vector3 posMasCercana = diosMapa.obtenerPuntoInteligente(CamaraFPS.Instance.camaraFramework.Position,posicionActual).getPosition();

            if(posMasCercana.Equals(posAnterior))
            {
                AnimatedBoss.Instance.comportamiento = new ComportamientoSeguir(posicionActual);

                posMasCercana = AnimatedBoss.Instance.comportamiento.proximoPunto(posicionActual);
            }

            posAnterior = posMasCercana;

            return posMasCercana;
        }
    }
}
