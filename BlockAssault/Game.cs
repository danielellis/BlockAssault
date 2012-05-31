using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BlockAssault {
    public class BlockAssaultGame : Game {
        private GraphicsDeviceManager graphics;
        private Board board;
        private KeyboardState prevKeyboardState, currKeyboardState;

        public SpriteBatch spriteBatch;

        public BlockAssaultGame() {
            graphics = new GraphicsDeviceManager (this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize () {
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent () {
            spriteBatch = new SpriteBatch(graphics.GraphicsDevice);

            board = new Board(this, spriteBatch);
            Components.Add(board);

            base.LoadContent();
        }

        protected override void Update (GameTime gameTime) {
            // TODO refactor this into a KeyboardInputManager (extends AbstractInputManager?)
            prevKeyboardState = currKeyboardState;
            currKeyboardState = Keyboard.GetState();

            Keys[] pressedKeys = currKeyboardState.GetPressedKeys();
            foreach (Keys k in pressedKeys) {
                if (!prevKeyboardState.IsKeyDown(k)) {
                    onKeyDown(k);
                }
            }

            base.Update (gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            graphics.GraphicsDevice.Clear(Color.Black);
            base.Draw(gameTime);
        }

        // TODO delegate?
        public void onKeyDown(Keys key) {
            switch (key) {
            case Keys.Up:
                board.MoveCursorUp();
                break;
            case Keys.Down:
                board.MoveCursorDown();
                break;
            case Keys.Left:
                board.MoveCursorLeft();
                break;
            case Keys.Right:
                board.MoveCursorRight();
                break;
            case Keys.Space:
                board.Swap();
                break;
            }
        }
    }
}
