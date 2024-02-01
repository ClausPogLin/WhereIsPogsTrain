namespace WhereIsPogsTrain
{
    public static class ConsoleHelper
    {
        public static void Print(string text, ConsoleColor color = default, int layerIndex = 0, string brforeTimer = "")
        {
            string _in = "";
            for (int i = 0; i < layerIndex; i++)
            {
                _in += "  ";
            }

            var _prevColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(brforeTimer + "[" + DateTime.UtcNow + " UTC] " + _in + text);
            Console.ForegroundColor = _prevColor;
        }

        public static void PrintInLineWithTimer(string text, ConsoleColor color = default, int layerIndex = 0)
        {
            string _in = "";
            for (int i = 0; i < layerIndex; i++)
            {
                _in += "  ";
            }

            var _prevColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.Write("[" + DateTime.UtcNow + " UTC] " + _in + text);
            Console.ForegroundColor = _prevColor;
        }

        public static void PrintInLine(string text, ConsoleColor color = default)
        {
            var _prevColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ForegroundColor = _prevColor;
        }

        public static List<int> ParseNumbers(string input)
        {
            // 使用String.Split方法将字符串拆分为字符串数组
            string[] numbers = input.Split(',');

            // 创建一个新的List集合
            List<int> result = new List<int>();

            // 遍历字符串数组，将每个字符串转换为数字并添加到List集合中
            for (int i = 0; i < numbers.Length; i++)
            {
                try
                {
                    // 将字符串转换为数字
                    int number = int.Parse(numbers[i]);

                    // 将数字添加到List集合中
                    result.Add(number);
                }
                catch (Exception)
                {
                    // 如果字符串不能转换为数字，则忽略该字符串
                    continue;
                }
            }

            // 返回List集合
            return result;
        }
    }
}