using System;
using Microsoft.Xna.Framework;

namespace BlockAssault {
    public enum BlockState {
        NORMAL,         // Not animating - ready to be moved or matched
        SLIDING_LEFT,   // Sliding left - locked until animation is complete
        SLIDING_RIGHT,  // Sliding left - locked until animation is complete
        FALLING,        // Falling due to "gravity" - locked until rest
        MATCHED,        // Matched - ready for the exploding animation
        EXPLODING,      // Explosion animation pending - will be empty upon completion
        EMPTY           // Empty - ready to be nulled out
    };

    public class Block {
        /// <summary>
        /// Constant defined width in pixels of one block.
        /// </summary>
        public const short WIDTH = 50;

        /// <summary>
        /// Constant defined height in pixels of one block
        /// </summary>
        public const short HEIGHT = 50;

        /// <summary>
        /// The different possible types of blocks.
        /// </summary>
        public static Color[] Types = {
            Color.Red,
            Color.Orange,
            Color.Green,
            Color.Yellow
        };

        /// <summary>
        /// Random number generator. Seeded once at runtime with current time.
        /// </summary>
        private static Random rand = new Random((int)(DateTime.Now.Ticks % int.MaxValue));

        /// <summary>
        /// Factory method that creates a block of a random type and returns it
        /// to the caller.
        /// </summary>
        /// <returns>
        /// The newly created block of random Color.
        /// </returns>
        public static Block GetRandomBlock() {
            return new Block(Types[rand.Next(Types.Length)]);
        }

        /// <summary>
        /// Helper method that checks to see if there are three blocks in a
        /// row that are in a NORMAL state that match.
        /// </summary>
        /// <returns>
        /// True if all three blocks match, false if any pair do not.
        /// </returns>
        public static bool ThreeBlocksMatch(Block block1, Block block2, Block block3) {
            return (block1 != null && block2 != null && block3 != null
                && block1.State == BlockState.NORMAL && block2.State == BlockState.NORMAL && block3.State == BlockState.NORMAL
                && block1.Type == block2.Type && block2.Type == block3.Type);
        }

        /// <summary>
        /// Gets or sets the block's type.
        /// </summary>
        /// <value>
        /// The type of the block (usually signified by the Color)
        /// </value>
        public Color Type { get; set; }

        /// <summary>
        /// Gets or sets the block state.
        /// </summary>
        /// <value>
        /// The state, always a member of the BlockState enum.
        /// </value>
        public BlockState State { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BlockAssault.Block"/>
        /// class. This method is private, use the factory method GetRandomBlock
        /// instead.
        /// </summary>
        private Block (Color type) {
            this.Type = type;
            this.State = BlockState.NORMAL;
        }
    }
}
