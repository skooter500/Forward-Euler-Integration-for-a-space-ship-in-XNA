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
    class EnemyDalek:Dalek
    {
        Quaternion current;
        Quaternion from;
        Quaternion to;
        float t;

        public EnemyDalek()
            : base()
        {
            current = Quaternion.Identity;
            from = Quaternion.Identity;
            to = Quaternion.Identity;
        }

        public override void Update(GameTime gameTime)
        {
            Vector3 toEnemy = XNAGame.Instance().Dalek.pos - pos;
            float timeDelta = (float) gameTime.ElapsedGameTime.TotalSeconds;
            float circle = 10.0f;
            float yaw = getYaw();
            //setYaw(yaw + timeDelta);

            if (toEnemy.Length() <= circle)
            {
                toEnemy.Normalize();
                float yawAngle = getYaw();

                float targetYaw = (float) Math.Acos(Vector3.Dot(basis, toEnemy));
                if (toEnemy.X > 0)
                {
                    targetYaw = (MathHelper.Pi * 2.0f) - targetYaw;
                }                

                if (Math.Abs(targetYaw - getYaw()) >= 0.01f)
                {
                    setYaw(getYaw() + (timeDelta));
                }
            }

            //toEnemy.Normalize();
            //look = toEnemy;

            if (look != basis)
            {
                float yawAngle = getYaw();
                float pitchAngle = getPitch();
                worldTransform = Matrix.CreateRotationX(pitchAngle) * Matrix.CreateRotationY(yawAngle) * Matrix.CreateTranslation(pos);
            }
            else
            {
                worldTransform = Matrix.CreateTranslation(pos);
            }
        }

        private void setYaw(float p)
        {
            look.X = -(float) Math.Sin(p);
            look.Y = 0.0f;
            look.Z = - (float) Math.Cos(p);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            XNAGame.Instance().SpriteBatch.DrawString(spriteFont, "Yaw: " + getYaw(), new Vector2(10, 100), Color.White);
            XNAGame.Instance().SpriteBatch.DrawString(spriteFont, "Look: " + look.X + " " + look.Y + " " + look.Z, new Vector2(10, 120), Color.White);
            
            //XNAGame.Instance().SpriteBatch.DrawString(spriteFont, "Yaw: " + getYaw(), new Vector2(10, 100), Color.White);
        }
    }
}
