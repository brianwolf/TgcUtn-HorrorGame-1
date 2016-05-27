﻿using Microsoft.DirectX;
using System;

namespace AlumnoEjemplos.MiGrupo
{
    internal class ButtonReturn:GameButton
    {
        private GameMenu menuAnterior;

        internal void setMenuAnterior(GameMenu menuAnterior)
        {
            this.menuAnterior = menuAnterior;
        }

        internal void init()
        {
            base.init("botonVolver", new Vector2(7.8f, 11f));
        }

        public override void execute(EjemploAlumno app, GameMenu menu)
        {
            app.menuActual = menuAnterior;

            //habria que destruir menu
        }

    }
}