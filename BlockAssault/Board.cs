using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BlockAssault {
    public class Board : DrawableGameComponent {
        public const short NUM_ROWS = 12;
        public const short NUM_COLS = 6;

        private Block[][] blocks;
        private Cursor cursor;
        private SpriteBatch spriteBatch;

        public int offsetX = 50,
                   offsetY = 50;

        // Textures
        private Texture2D blockSprite, solidColor;

        public Board (Game game, SpriteBatch spriteBatch) : base(game) {
            this.spriteBatch = spriteBatch;
        }

        public override void Initialize () {
            // Initialize grid of blocks
            blocks = new Block[NUM_ROWS][];
            for (int i = 0; i < NUM_ROWS; i++) {
                blocks[i] = new Block[NUM_COLS];
            }

            // Set the cursor position
            cursor = new Cursor(0, 0);

            // Fill the spaces with blocks
            FillBoard();

            base.Initialize ();
        }

        protected override void LoadContent () {
            blockSprite = this.Game.Content.Load<Texture2D>("img/tennisBall");

            solidColor = new Texture2D(this.Game.GraphicsDevice, 1, 1);
            solidColor.SetData(new Color[] {Color.White});

            base.LoadContent ();
        }

        public override void Update (GameTime gameTime) {
            base.Update(gameTime);
        }

        public override void Draw (GameTime gameTime) {
            spriteBatch.Begin();

            // First, draw the Cursor
            // TODO This should be drawn after if the graphic changes to an overlay
            spriteBatch.Draw(
                solidColor,
                new Rectangle(offsetX + (cursor.Column * Block.WIDTH), offsetY + (cursor.Row * Block.HEIGHT), 2 * Block.WIDTH, Block.WIDTH),
                Color.White);

            for (int row = 0 ; row < NUM_ROWS; row++) {
                for (int column = 0; column < NUM_COLS; column++) {
                    spriteBatch.Draw(
                        blockSprite,
                        new Rectangle(offsetX + (column * Block.WIDTH), offsetY + (row * Block.HEIGHT), Block.WIDTH, Block.HEIGHT),
                        blocks[row][column].Type);
                }
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// Fills the board from empty
        /// </summary>
        private void FillBoard() {
            for (int i = 0; i < NUM_ROWS; i++) {
                fillRowFromBottom ();
            }
        }

        /// <summary>
        /// Creates a row of blocks and adds them to the bottom of the board.
        /// This function checks each block to make sure that it doesn't
        /// accidentally make a matching 3 in a row, which would cause an
        /// immediate animation.
        /// </summary>
        private void fillRowFromBottom() {
            // First, move every row up one row

            Block[] tmp = blocks[0];
            for (short i = 0; i < blocks.Length - 1; i++) {
                blocks[i] = blocks[i+1];
            }
            blocks[NUM_ROWS - 1] = tmp;

            // Now, create the new row
            for (short i = 0; i < NUM_COLS; i++) {
                bool getNewBlock;
                do {
                    getNewBlock = false;
                    blocks[NUM_ROWS - 1][i] = Block.GetRandomBlock();

                    // Choose another type of block if the two above it are the same type
                    if (Block.ThreeBlocksMatch(blocks[NUM_ROWS - 1][i], blocks[NUM_ROWS - 2][i], blocks[NUM_ROWS - 3][i])) {
                        getNewBlock = true;
                    }

                    // Choose another type of block if the two to its immediate left are the same type
                    if (i >= 2 && Block.ThreeBlocksMatch(blocks[NUM_ROWS - 1][i], blocks[NUM_ROWS - 1][i - 1], blocks[NUM_ROWS - 1][i - 2])) {
                        getNewBlock = true;
                    }
                } while (getNewBlock);
            }
        }

        public void Swap() {
            Block tmp = blocks[cursor.Row][cursor.Column];
            blocks[cursor.Row][cursor.Column] = blocks[cursor.Row][cursor.Column + 1];
            blocks[cursor.Row][cursor.Column + 1] = tmp;
        }

        public void MoveCursorLeft() {
            cursor.MoveLeft();
        }

        public void MoveCursorRight() {
            cursor.MoveRight();
        }

        public void MoveCursorUp() {
            cursor.MoveUp();
        }

        public void MoveCursorDown() {
            cursor.MoveDown();
        }
    }
}
