// See https://aka.ms/new-console-template for more information
// read from file
// load data into a 2 dimentional array?
// Specify the path to the file you want to read

using System.Runtime.CompilerServices;

string filePath = "C:\\Users\\Creon\\Desktop\\MyData.txt";
char[,] myData = new char[140,140];
//array that contains -1 for ., 0 for a digit, 1 for a symbol and 2 for a *
int[,] charType = new int[140, 140];

//array that stores the total value of each number in all the locations that number exists across
int[,] totalForNumber = new int[140, 140];

int sum = 0;
int gearSum = 0;

// Check if the file exists
if (File.Exists(filePath))
{
    // Open the file for reading
    using (StreamReader reader = new StreamReader(filePath))
    {
        int lineCount = 0;
        // Read and display the content line by line
        while (!reader.EndOfStream)
        {
            string line = reader.ReadLine();
            
            for(int i = 0; i < line.Length; i++)
            {
                char parsedChar = line[i];
                Console.Write(parsedChar);
                myData[lineCount,i] = parsedChar;

                //identify the character type of that character
                charType[lineCount, i] = parseCharType(parsedChar);
                
            }
            Console.Write("\n");
            lineCount++;
        }
    }
}
else
{
    Console.WriteLine("File not found: " + filePath);
}

//parse data array to identify numbers
//parse row
for (int i = 0; i < myData.GetLength(0); i++)
{
    //parse column
    for(int j = 0; j < myData.GetLength(1); j++)
    {
        //get single character
        char parsedChar = myData[i,j];
        int numberLength = 0;

        //if you find a number
        if(char.IsNumber(parsedChar))
        {
            int row = i; // save the row of the number
            int start = j; // save the starting point of the number
            int end; // save the ending point of the number
            int.TryParse(parsedChar.ToString(), out int numberBuilder);

            //start tracking how long the number is so we can store the values
            //in the totalForNumber array
            numberLength = 0;
            
            //check if the next char is also a digit
            while (char.IsNumber(myData[i,j+1]))
            {
                numberLength++;
                
                //increment j to the next item
                j++;

                //shift digits over, add next digit
                numberBuilder *= 10;
                int.TryParse(myData[i, j].ToString(), out int nextDigit); 
                numberBuilder += nextDigit;

                if (j == myData.GetLength(1) -1) break;
            }

            //go back through all the digit locations that were part of the
            //number and add the total for gear calculations
            for(int k = numberLength; k >= 0; k--)
            {
                totalForNumber[i, j - k] = numberBuilder;
            }

            end = j;

            //if the number doesnt have any symbols around it add it to the total
            if (isValidNumber(row, start, end))
            {
                sum += numberBuilder;
            }
        }
    }
}
for(int i = 0; i < 10 ; i++)
{
    Console.WriteLine();
}
Console.WriteLine("=========================================================================================================");
for (int i = 0; i < 10; i++)
{
    Console.WriteLine();
}


//loop through to check for gears and add the total 
//parse row
for (int i = 0; i < myData.GetLength(0); i++)
{
    //parse column
    for (int j = 0; j < myData.GetLength(1); j++)
    {

        //if there is a symbol
        if (charType[i, j] == 2)
        {
            //determine if the * is a gear
            if (isGear(i, j))
            {
                gearSum += getGearSum(i, j);
                Console.Write('G');
            }
            else if (charType[i, j] == 2) Console.Write('*');
        }
        else
        {
            if (charType[i, j] == -1) Console.Write('.');
            else if (charType[i, j] == 0) Console.Write(myData[i,j]);
            else if (charType[i, j] == 1) Console.Write('.');
            

        }
    }
    Console.WriteLine();
}

Console.WriteLine("The sum is " + sum);

Console.WriteLine("The gear sum is " + gearSum);


Console.WriteLine("============================================================================");

bool isValidNumber(int row, int startCol, int endCol)
{
    bool rowContainsSymbol = false;

    //check row before
    rowContainsSymbol |= doesRowContainSymbol(row-1, startCol - 1, endCol + 1);
    //check current row
    rowContainsSymbol |= doesRowContainSymbol(row, startCol - 1, endCol + 1);
    //check row after
    rowContainsSymbol |= doesRowContainSymbol(row + 1, startCol - 1, endCol + 1);

    return rowContainsSymbol;
}

bool doesRowContainSymbol(int row, int startCol, int endCol)
{
    // do not attempt on an invalid row
    if (row < 0 || row >= myData.GetLength(0))
    {
        return false;
    }

    bool result = false;

    //protect from out of bounds exception
    if (startCol < 0) startCol = 0;
    if (endCol == myData.GetLength(1)) endCol -= 2;

    // search through the defined section for a symbol
    for(int i = startCol; i <= endCol; i++)
    {
        //if (isSymbol(myData[row, i]))
        if (charType[row,i] > 0)
        {
            result = true;
        }
    }
    

    return result;
}


// identify symbols
// pass in row and column
bool isSymbol(char itsChar)
{
    bool isSymbol = false;
    if(char.IsNumber(itsChar) == false
        && itsChar != '.')
    {
        isSymbol = true;
    }


    return isSymbol;
}

//-1 for period
//0 for digit
//1 for symbol
//2 for *
int parseCharType(char itsChar)
{
    int charType; //-1 for a period
    if(itsChar == '.')
    {
        charType = -1;
    }
    else if(char.IsNumber(itsChar))
    {
        charType = 0;
    }
    else if(itsChar == '*')
    {
        charType = 2;
    }
    else
    {
        charType = 1;
    }
    return charType;

}

// returns how many numbers are in the area specified
int howManyNumbersInRange(int row, int startCol, int endCol)
{
    if (row < 0 || row >= myData.GetLength(0))
    {
        return 0;
    }

    int result = 0;

    //protect from out of bounds exception
    if (startCol < 0) startCol = 0;
    if (endCol == myData.GetLength(1)) endCol -= 2;

    bool firstNum = false; //if there is one number set to true
    bool isSpace = false; //if there is a space after one number set to true
    bool secondNum = false; //if there is a number after the space

    // search through the defined section for a symbol
    for (int i = startCol; i <= endCol; i++)
    {
        if (charType[row, i] == 0)
        {
            if (isSpace == false)
            {
                firstNum = true;
            }
            else
            {
                secondNum = true;
            }
        }
        if (charType[row, i] != 0)
        {
            //if there was some numbers and then a symbol, count this as the space
            if (firstNum == true)
            {
                isSpace = true;
            }
        }

    }
    if (firstNum) result = 1;
    if (secondNum) result = 2;

    return result;
}

bool isGear(int row, int column)
{
    int numberOfNumbers = 0;

    //count the number of numbers above
    numberOfNumbers += howManyNumbersInRange(row-1, column - 1, column + 1);

    //count the number of numbers on this row
    numberOfNumbers += howManyNumbersInRange(row, column - 1, column + 1);

    //count the number of numbers on the row below
    numberOfNumbers += howManyNumbersInRange(row+1, column - 1, column + 1);

    return numberOfNumbers == 2;
}

int getGearSum(int row, int column)
{
    int result = 1;
    int row1 = 0;
    int row2 = 0;
    int row3 = 0;

    //count the number of numbers above
    row1 = getValueOfNumbersInRange(row - 1, column - 1, column + 1);

    //count the number of numbers on this row
    row2 = getValueOfNumbersInRange(row, column - 1, column + 1);

    //count the number of numbers on the row below
    row3 = getValueOfNumbersInRange(row + 1, column - 1, column + 1);

    if (row1 > 0) result *= row1;
    if (row2 > 0) result *= row2;
    if (row3 > 0) result *= row3;

    if (row == 138)
    {
        bool asdfsad = true;
    }

    return result;
}

int getValueOfNumbersInRange(int row, int startCol, int endCol)
{
    if (row < 0 || row >= myData.GetLength(0))
    {
        return 0;
    }

    int result = 0;

    //protect from out of bounds exception
    if (startCol < 0) startCol = 0;
    if (endCol == myData.GetLength(1)) endCol -= 2;

    bool firstNum = false; //if there is one number set to true
    bool isSpace = false; //if there is a space after one number set to true
    bool secondNum = false; //if there is a number after the space

    // search through the defined section for a symbol
    for (int i = startCol; i <= endCol; i++)
    {
        if (charType[row, i] == 0)
        {
            if (firstNum != true)
            {
                firstNum = true;
                result = totalForNumber[row, i];
            }
            else if(isSpace == true
                    && secondNum != true)
            {
                secondNum = true;
                result *= totalForNumber[row, i];
            }
        }
        if (charType[row, i] != 0)
        {
            //if there was some numbers and then a symbol, count this as the space
            if (firstNum == true)
            {
                isSpace = true;
            }
        }

    }

    return result;
}