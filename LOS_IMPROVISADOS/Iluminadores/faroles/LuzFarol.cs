﻿using TgcViewer;
using Microsoft.DirectX;
using TgcViewer.Utils.TgcSceneLoader;
using Microsoft.DirectX.Direct3D;
using System.Drawing;
using AlumnoEjemplos.LOS_IMPROVISADOS.Iluminadores.IyCA;
using TgcViewer.Utils.TgcGeometry;

namespace AlumnoEjemplos.LOS_IMPROVISADOS.Iluminadores.faroles
{
    class LuzFarol : ALuz
    {
        public LuzFarol(TgcScene tgcEscena, CamaraFPS camaraFPS) : base(tgcEscena, camaraFPS)
        {
        }

        private TgcPlaneWall fondoNegro;
        private const float distanciaFondo = 5000;
        
        public override void init()
        {
            GuiController.Instance.Modifiers.addColor("farolColor", Color.LightYellow);
            GuiController.Instance.Modifiers.addFloat("farolIntensidad", 0f, 150f, 19f);
            GuiController.Instance.Modifiers.addFloat("farolAtenuacion", 0.1f, 2f, 0.1f);
            GuiController.Instance.Modifiers.addFloat("farolEspecularEx", 0, 20, 4f);

            //Modifiers de material
            GuiController.Instance.Modifiers.addColor("farolEmissive", Color.Black);
            GuiController.Instance.Modifiers.addColor("farolAmbient", Color.LightYellow);
            GuiController.Instance.Modifiers.addColor("farolDiffuse", Color.White);
            GuiController.Instance.Modifiers.addColor("farolSpecular", Color.LightYellow);
            
			//Inicio el fondoNegro
			TgcTexture texturaFondo = TgcTexture.createTexture(GuiController.Instance.AlumnoEjemplosDir +
			                                                   "Media\\mapa\\fondoNegro.png");
			fondoNegro = new TgcPlaneWall(new Vector3(0,0,0),
			                              new Vector3(100000,100000,100000),
			                              TgcPlaneWall.Orientations.XYplane,
			                              texturaFondo);
            
        }
        
        private void updateFondo()
        {
        	Vector3 dirCamara = camaraFPS.camaraFramework.viewDir;
        	dirCamara.Y = 0;
        	dirCamara.TransformCoordinate(Matrix.RotationY(FastMath.ToRad(400)));
        	Vector3 posCamara = camaraFPS.camaraFramework.Position;
        	posCamara.Y = 0;
        	
        	//Cambio la orientacion de la pared
        	if(FastMath.Abs(dirCamara.X)>FastMath.Abs(dirCamara.Z))
        	{
        		fondoNegro.Orientation = TgcPlaneWall.Orientations.YZplane;
        	}else{
        		fondoNegro.Orientation = TgcPlaneWall.Orientations.XYplane;
        	}
        	
        	//Cambio la posicion
        	fondoNegro.Origin = posCamara + dirCamara * distanciaFondo;
        	fondoNegro.updateValues();
        }

        public override void render()
        {
            Effect currentShader = GuiController.Instance.Shaders.TgcMeshPointLightShader;

            //Dibujo el fondo para evitar el azul
            updateFondo();
            fondoNegro.render();
            
            foreach (TgcMesh mesh in tgcEscena.Meshes)
            {
                mesh.Effect = currentShader;
                mesh.Technique = GuiController.Instance.Shaders.getTgcMeshTechnique(mesh.RenderType);
            }
            
            foreach (TgcMesh mesh in tgcEscena.Meshes)
            {
            	//Primero dibujo un sprite negro en toda la pantalla para evitar el azul feo
            	GuiController.Instance.Drawer2D.beginDrawSprite();
	            fondoNegro.render();
	            GuiController.Instance.Drawer2D.endDrawSprite();
            	
                //Cargar variables shader de la luz
                mesh.Effect.SetValue("lightColor", ColorValue.FromColor((Color)GuiController.Instance.Modifiers["farolColor"]));
                mesh.Effect.SetValue("lightPosition", TgcParserUtils.vector3ToFloat4Array(camaraFPS.posicion));
                mesh.Effect.SetValue("lightIntensity", (float)GuiController.Instance.Modifiers["farolIntensidad"]);
                mesh.Effect.SetValue("lightAttenuation", (float)GuiController.Instance.Modifiers["farolAtenuacion"]);

                //Cargar variables de shader de Material. El Material en realidad deberia ser propio de cada mesh. Pero en este ejemplo se simplifica con uno comun para todos
                mesh.Effect.SetValue("materialEmissiveColor", ColorValue.FromColor((Color)GuiController.Instance.Modifiers["farolEmissive"]));
                mesh.Effect.SetValue("materialAmbientColor", ColorValue.FromColor((Color)GuiController.Instance.Modifiers["farolAmbient"]));
                mesh.Effect.SetValue("materialDiffuseColor", ColorValue.FromColor((Color)GuiController.Instance.Modifiers["farolDiffuse"]));
                mesh.Effect.SetValue("materialSpecularColor", ColorValue.FromColor((Color)GuiController.Instance.Modifiers["farolSpecular"]));
                mesh.Effect.SetValue("materialSpecularExp", (float)GuiController.Instance.Modifiers["farolEspecularEx"]);

                if( ( (mesh.BoundingBox.PMax*0.5f - mesh.BoundingBox.PMin *0.5f) +mesh.BoundingBox.PMin - camaraFPS.posicion).Length() < 5000){
                	
               		 mesh.render();
                }
            }
        }
    }
}
