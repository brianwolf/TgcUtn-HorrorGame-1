﻿/*
 * Created by SharpDevelop.
 * User: Lelouch
 * Date: 07/06/2016
 * Time: 19:13
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using Microsoft.DirectX;
using AlumnoEjemplos.LOS_IMPROVISADOS.Objetos;
using TgcViewer.Utils.TgcGeometry;

namespace AlumnoEjemplos.LOS_IMPROVISADOS
{
	/// <summary>
	/// Description of HardCodeadorObjetos.
	/// </summary>
	public static class HardCodeadorObjetos
	{
		public static List<Accionable> HardCodearObjetos()
		{
			List<Accionable> lista = new List<Accionable>();

			
			MapaObjeto mapa = new MapaObjeto();
			mapa.cambiarVectores(new Vector3(2179.103f,8.6139f,10272.55f), new Vector3(1,1,1));
			mapa.getMesh().rotateY(FastMath.PI/4);
			lista.Add(mapa);
			
			#region Barriles
			Barril barril1 = new Barril();
			barril1.cambiarVectores(new Vector3(20245.44f, 77.6337f, 9634.468f), new Vector3(1f,1f,1f));
			lista.Add(barril1);
			#endregion Barriles
			
			#region Baterias
			Bateria bateria1 = new Bateria();
			bateria1.cambiarVectores(new Vector3(1060.653f,184.4886f,6661.554f), new Vector3(0.1f,0.1f,0.1f));
			lista.Add(bateria1);
			
			Bateria bateria2 = new Bateria();
			bateria2.cambiarVectores(new Vector3(5342.72f,192.2363f,10792f), new Vector3(0.1f,0.1f,0.1f));
			lista.Add(bateria2);
			
			Bateria bateria3 = new Bateria();
			bateria3.cambiarVectores(new Vector3(5326.568f,15.2f,12911.81f), new Vector3(0.1f,0.1f,0.1f));
			lista.Add(bateria3);
			#endregion Baterias
			
			#region Puertas
			Puerta puerta1 = new Puerta(-1,0);
			puerta1.cambiarVectores(new Vector3(2479.43f,0.0f,11201.52f), new Vector3(1.5f,0.45f,0.7f));
			lista.Add(puerta1);
			#endregion Puertas
			
			return lista;
		}
	}
}
