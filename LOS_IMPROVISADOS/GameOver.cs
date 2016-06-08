﻿/*
 * Created by SharpDevelop.
 * User: Lelouch
 * Date: 07/06/2016
 * Time: 12:49
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using Microsoft.DirectX;
using TgcViewer;
using TgcViewer.Utils._2D;
using TgcViewer.Utils.TgcSceneLoader;
using TgcViewer.Utils.TgcGeometry;
using TgcViewer.Utils.Sound;

namespace AlumnoEjemplos.LOS_IMPROVISADOS
{
	/// <summary>
	/// Description of GameOver.
	/// </summary>
	public class GameOver
	{
		TgcSprite gameOverScreen;
		TgcStaticSound sonidoGameOver;
		TgcStaticSound sonidoBoss;
		
		bool activado = false;
		
		float timer = 0;
		bool reproducido = false;
		
		//Para la animacion
		float escaladoMax;
		float escalado = 0;
		const float speed = 0.5f;
		
		#region singleton
		private static GameOver instance;
		public static GameOver Instance
		{
			get{
				if(instance == null)instance = new GameOver();
				return instance;
			}
		}
		
		private GameOver()
		{
			gameOverScreen = new TgcSprite();
			gameOverScreen.Texture = TgcTexture.createTexture(
				GuiController.Instance.AlumnoEjemplosDir + "Media\\GameOver\\youDied.png");

			sonidoGameOver = new TgcStaticSound();
			sonidoGameOver.loadSound(
				GuiController.Instance.AlumnoEjemplosDir + "Media\\GameOver\\st_death.wav");
			
			sonidoBoss = new TgcStaticSound();
			sonidoBoss.loadSound(
				GuiController.Instance.AlumnoEjemplosDir + "Media\\GameOver\\tuAlmaEsMiaFuerte.wav");
			
			//Calculo el escalado max
			Size pantallaSize = GuiController.Instance.Panel3d.Size;
			Size gameOverSize = gameOverScreen.Texture.Size;
			
			float widthScale = (float)pantallaSize.Width / (float)gameOverSize.Width;
            float heightScale = (float)pantallaSize.Height / (float)gameOverSize.Height;
            //Respeto el aspect Ratio
            escaladoMax = FastMath.Min(widthScale,heightScale);
		}
		#endregion singleton
		
		public void render(float elapsedTime)
		{
			if(activado)
			{
				posicionarSprite(elapsedTime);
				
	            GuiController.Instance.Drawer2D.beginDrawSprite();
	            gameOverScreen.render();
				GuiController.Instance.Drawer2D.endDrawSprite();
				
				timer+= elapsedTime;
				
				if(timer > 2 && !reproducido)
				{
					sonidoBoss.play();
					reproducido = true;
				}
				
			}
		}
		
		private void posicionarSprite(float elapsedTime)
		{
			Size pantallaSize = GuiController.Instance.Panel3d.Size;
			Size gameOverSize = gameOverScreen.Texture.Size;
			
			escalado += speed * elapsedTime;
			
			escalado = FastMath.Min(escaladoMax,escalado);
			
			gameOverScreen.Scaling = new Vector2(escalado, escalado);
			
			//Lo pongo en el medio de la pantalla
			gameOverScreen.Position = new Vector2(
				pantallaSize.Width/2 - gameOverSize.Width * escalado / 2,
				pantallaSize.Height/2 - gameOverSize.Height * escalado / 2);
		}
		
		public void activar()
		{
			activado = true;
			sonidoGameOver.play();
		}
		
	}
}