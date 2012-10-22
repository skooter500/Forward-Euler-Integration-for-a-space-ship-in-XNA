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

namespace Steering
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class XNAGame : Microsoft.Xna.Framework.Game
    {
        static XNAGame instance = null;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private Ground ground;

        public SpriteBatch SpriteBatch
        {
            get { return spriteBatch; }
            set { spriteBatch = value; }
        }
        private Camera camera;
        List<Entity> children = new List<Entity>();

        public List<Entity> Children
        {
            get { return children; }
            set { children = value; }
        }
        private Dalek dalek;

        public Dalek Dalek
        {
            get { return dalek; }
            set { dalek = value; }
        }

        public static XNAGame Instance()
        {
            return instance;
        }

        public XNAGame()
        {
            instance = this;
            graphics = new GraphicsDeviceManager(this);

            graphics.PreferredBackBufferWidth = 1024;
            graphics.PreferredBackBufferHeight = 768;
            graphics.PreferMultiSampling = true;
            graphics.SynchronizeWithVerticalRetrace = true;
            graphics.ApplyChanges();

            Content.RootDirectory = "Content";
        }
        
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            camera = new Camera();
            int midX = GraphicsDeviceManager.DefaultBackBufferHeight / 2;
            int midY = GraphicsDeviceManager.DefaultBackBufferWidth / 2;
            Mouse.SetPosition(midX, midY);

            dalek = new Dalek();
            dalek.pos = new Vector3(0, 0, 0);
            dalek.DrawAxis = true;
            children.Add(dalek);

            EnemyDalek enemy = new EnemyDalek();
            enemy.pos = new Vector3(10, 0, -10);
            enemy.DrawAxis = true;
            children.Add(enemy);
                                   
            children.Add(camera);
            ground = new Ground();                        
            children.Add(ground);

            Fighter fighter = new Fighter();
            fighter.pos.X = 0; fighter.pos.Y = 20; fighter.pos.Z = 0;
            children.Add(fighter);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            foreach (Entity child in children)
            {
                child.LoadContent();
            }

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            foreach (Entity child in children)
            {
                child.UnloadContent();
            }

        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            KeyboardState keyState = Keyboard.GetState();
            if (keyState.IsKeyDown(Keys.Escape))
            {
                this.Exit();
            }

            for (int i = 0; i < children.Count; i++)
            {
                children[i].Update(gameTime);
            }

            /*foreach (Entity child in children)
            {
                child.Update(gameTime);
            }*/
            //camera.pos = camFighter.pos;
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            foreach (Entity child in children)
            {
                DepthStencilState state = new DepthStencilState();
                state.DepthBufferEnable = true;                
                GraphicsDevice.DepthStencilState = state;
                child.Draw(gameTime);
            }
            spriteBatch.End();            
        }

        public Camera Camera
        {
            get
            {
                return camera;
            }
            set
            {
                camera = value;
            }
        }

        public GraphicsDeviceManager GraphicsDeviceManager
        {
            get
            {
                return graphics;
            }
        }
    }
}
