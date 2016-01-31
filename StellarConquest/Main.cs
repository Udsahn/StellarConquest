#region Liscense
/*

The MIT License (MIT)

Copyright (c) 2016 Udsahn

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.

*/
#endregion

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System;

using StellarConquest.Graphics;

using Udsahn.Graphics;
using Udsahn.Graphics.FontLoader;

using StellarConquest.Graphics.Animation;
using StellarConquest.Graphics.Text;
using StellarConquest.Core.Data;

using System.Collections.Generic;

namespace StellarConquest
{
    public static class ColorScheme
    {
        public static Color NormalText = new Color(170, 165, 184);
        public static Color NormalBackground = new Color(28, 0, 44);
    }

    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Main : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Random r = new Random();
        
        // Timer
        int highlightCountdown = 600;
        int lowlightCountdown = 100;

        int timer;

        // Title Color
        Color titleColor;

        // Font loader
        Font boldFont;
        Font microFont;
        Font macroFont; // Not created yet.

        // Font Manager (Multiple fonts in one object)
        FontManager fm;

        //KeyframeList tester
        KeyframeList kl;
        Keyframe curentFrame;

        // Keyframe Manager test.
        KeyframeManager km = new KeyframeManager();

        // Message test.
        Message m;

        Texture2D fontImage;

        public Main()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

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
            fm = new FontManager();

            // TODO: use this.Content to load your game content here
            fontImage = Content.Load<Texture2D>("BoldTypeface_2.png");
            boldFont = new Font("Custom Bold Font", fontImage, new Vector2(11, 20));
            microFont = new Font("Custom micro Font", Content.Load<Texture2D>("MicroFont.png"), new Vector2(6, 10), true);

            // Adds a new font to the Font Manager.
            fm.AddFontsheet("DEFAULT", Content.Load<Texture2D>("MicroFont.png"), 6, 10, true);
            fm.AddFontsheet("MicroFont", Content.Load<Texture2D>("MicroFont.png"), 6, 10, true);


            kl = new KeyframeList();
            kl.AddKeyframe(1000, Color.Red);
            kl.AddKeyframe(500, Color.Green);
            kl.AddKeyframe(250, Color.Blue);
            kl.AddKeyframe(2750, Color.Yellow);
            kl.AddKeyframe(5000, Color.White);
            kl.Cycle = KeyframeCycle.Repeat;
            kl.Start();

            // Adds a new font to the Font Manager that
            // was already instantiated.
            fm.AddFontsheet(boldFont);

            // Uses custom Builder.
            using (KeyframeBuilder kb = new KeyframeBuilder())
            {
                kb.AddKeyframe(new Keyframe(100, Color.Red, new Vector2(10, 350), new Rectangle(0, 0, 0, 0)));
                kb.AddKeyframe(new Keyframe(100, Color.Purple, new Vector2(10, 351), new Rectangle(0, 0, 0, 0)));
                kb.AddKeyframe(new Keyframe(100, Color.Blue, new Vector2(10, 352), new Rectangle(0, 0, 0, 0)));
                kb.AddKeyframe(new Keyframe(100, Color.Green, new Vector2(10, 353), new Rectangle(0, 0, 0, 0)));
                kb.AddKeyframe(new Keyframe(100, Color.Yellow, new Vector2(10, 352), new Rectangle(0, 0, 0, 0)));
                kb.AddKeyframe(new Keyframe(100, Color.Orange, new Vector2(10, 351), new Rectangle(0, 0, 0, 0)));

                // Adds one keyframe to the manager.
                km.Add(kb.Result, "Test");
            }

            km.StartAll();

            m = new Message("This is a test.");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            // Updates the timer for the title text highlighting.
            UpdateLogic(gameTime);

            // Updates the keyframes.
            kl.Update(gameTime);

            // Update Keyframe Manager.
            km.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(StellarConquest.Graphics.ColorScheme.WindowBackground);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            // Stock Fontsheet Render not using the custom parser.
            spriteBatch.Draw(fontImage, new Vector2(10, 30), Color.Red);

            // Font Demo Render
            microFont.Demo(new Vector2(10, 30 + boldFont.TypeSize.Height), spriteBatch);

            // Version info rendering
            if (timer > highlightCountdown)
                titleColor = Color.White;
            else
                titleColor = Color.Red;

            microFont.DrawText("| || Font Loader Version 0.001 Alpha || | ---- DEMO SAMPLE ----", new Vector2(5, 2), spriteBatch, titleColor);

            // Actual demo of font rendering.
            boldFont.DrawText("THIS IS AN ACTUAL TEST OF FONT RENDERING", new Vector2(10, 120), spriteBatch);
            boldFont.DrawText("IT USES THE CUSTOM FONT I MADE AND A CUSTOM PARSER AND RENDERER", new Vector2(10, 140), spriteBatch);

            // Font rendering of the specific font names given.
            boldFont.DrawText(boldFont.Name.ToUpper(), new Vector2(10, 200), spriteBatch);
            microFont.DrawText(microFont.Name.ToUpper(), new Vector2(10, 200 + boldFont.TypeSize.Height), spriteBatch);

            // Uses the Font Manager to print text to the screen.
            fm.DrawText("MicroFont", "This is a test of the Font Manager using stringed tags.", new Vector2(10, 250), spriteBatch, Color.Yellow);
            fm.DrawText(1, "This is a test of the Font Manager using an index.".ToUpper(), new Vector2(10, 265), spriteBatch, Color.LightYellow);

            // Font Manager helper methods to aid in font discovery.
            fm.FindIndex("Not_here");
            fm.FindTag(10);

            // Draw information from the KeyframeList.
            curentFrame = kl.Keyframe;

            fm.DrawText(0, string.Concat("Frame Index: ", kl.KeyframeIndex, ", Curent time: ", kl.Elapsed, "ms, State: ", kl.State.ToString(), ", Cycle Option: ", kl.Cycle.ToString(), " || Delay -- ", curentFrame.Delay, ", Color ~~ ", curentFrame.Tint.ToString()),
                        new Vector2(10, 300), spriteBatch, curentFrame.Tint);

            fm.DrawText(0, km[0].Keyframe.Delay + " : " + km[0].Elapsed, km[0].Keyframe.Position, spriteBatch, km[0].Keyframe.Tint);
            fm.DrawText(m.Font, m.Text, new Vector2(10, 400), spriteBatch, Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        protected void UpdateLogic(GameTime gameTime)
        {
            timer += gameTime.ElapsedGameTime.Milliseconds;

            if (timer > highlightCountdown + lowlightCountdown)
            {
                timer = 0;
            }
        }
    }
}
