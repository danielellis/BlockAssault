using System;

namespace BlockAssault {
    public class Cursor {
        private short row, column;

        public short Row { get { return row; } }
        public short Column { get { return column; } }

        public Cursor (short row, short column) {
            this.row = row;
            this.column = column;
        }

        public void MoveLeft() {
            if (column > 0) {
                column--;
            }
        }

        public void MoveRight() {
            // The cursor position represents the position of the left block
            // that it surrounds, so it must be an extra block away from the
            // right edge of the board.
            if (column < Board.NUM_COLS - 2) {
                column++;
            }
        }

        public void MoveDown() {
            if (row < Board.NUM_ROWS - 1) {
                row++;
            }
        }

        public void MoveUp() {
            if (row > 0) {
                row--;
            }
        }
    }
}
