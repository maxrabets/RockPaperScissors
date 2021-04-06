using System;
using System.Linq;
using System.Security.Cryptography;

namespace RockPaperScissors
{
    public class Game
    {
        private RandomNumberGenerator generator = RandomNumberGenerator.Create();
        public byte[] Key { get; private set; }
        private byte[] computerMoveBytes = new byte[4];
        public int ComputerMove { get; private set; }
        public byte[] Hash { get; private set; }
        public string[] Items { get; set; }

        public Game(string[] items)
        {
            Items = items;
            Key = new byte[16];
        }

        public static bool CheckItems(string[] items)
        {
            if (items.Length < 3 || items.Length % 2 == 0 
                || items.Where(item => items.Count(i => i == item) > 1).Count() > 1)
                return false;
            return true;
        }

        public int MakeComputerMove()
        {
            generator.GetNonZeroBytes(Key);
            generator.GetNonZeroBytes(computerMoveBytes);
            ComputerMove = BitConverter.ToInt32(computerMoveBytes, 0);
            ComputerMove = Math.Abs(ComputerMove % Items.Length);
            computerMoveBytes = BitConverter.GetBytes(ComputerMove);
            HMACSHA256 hmac = new HMACSHA256(Key);
            Hash = hmac.ComputeHash(computerMoveBytes);
            return ComputerMove;
        }
        
        public int CompareMoves(int move1, int move2)
        {
            if (move1 == move2)
                return 0;
            int half = Items.Length / 2;
            if(move1 > move2)
            {
                if(move1 - move2 <= half)
                    return 1;
                else
                    return 2;
            }
            else
            {
                if (move2 - move1 <= half)
                    return 2;
                else
                    return 1;
            }
        }
    }
}
