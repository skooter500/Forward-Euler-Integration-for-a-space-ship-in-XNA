using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace Steering
{
    class Fighter:Entity
    {
        float mass;

        public Fighter()
        {
            force = Vector3.Zero;
            mass = 1.0f;
        }

        public override void LoadContent()
        {
            model = XNAGame.Instance().Content.Load<Model>("fighter");
        }

        public void push(Vector3 force)
        {
            this.force += force;
        }


        public override void Update(GameTime gameTime)
        {
            KeyboardState state = Keyboard.GetState();
            if (state.IsKeyDown(Keys.I))
            {
                push(look * 50f);
            }
            if (state.IsKeyDown(Keys.K))
            {
                push(-look * 50f);
            }
            if (state.IsKeyDown(Keys.J))
            {
                push(right * 50f);
            }
            if (state.IsKeyDown(Keys.L))
            {
                push(-right * 50f);
            }

            float timeDelta = (float) gameTime.ElapsedGameTime.TotalSeconds;
            
            Vector3 acceleration = force / mass;
            velocity = velocity + acceleration * timeDelta;
            pos = pos + velocity * timeDelta;
            if (velocity.Length() > 0)
            {
                look = velocity;
                look.Normalize();

                right = Vector3.Cross(up, look);
                right.Normalize();
            }
            force = Vector3.Zero;
            base.Update(gameTime);
        }

        public override void UnloadContent()
        {
            
        }

        public override void Draw(GameTime gameTime)
        {
            // Draw the mesh
            if (model != null)
            {
                foreach (ModelMesh mesh in model.Meshes)
                {
                    foreach (BasicEffect effect in mesh.Effects)
                    {
                        effect.EnableDefaultLighting();
                        effect.PreferPerPixelLighting = true;
                        effect.World = worldTransform;
                        effect.Projection = XNAGame.Instance().Camera.getProjection();
                        effect.View = XNAGame.Instance().Camera.getView();
                    }
                    mesh.Draw();
                }
            }
        }
    }
}
