// Aviya levi 322286436
// Computer Sience Introduction
//Exercise#2 Question#6
// A program that receives two positive integers and prints the sum, the diffrence' the multiplication and the prtion
#include <iostream>
using namespace std;
int main()
{
	int x;
	int y;
	cout << "enter two numbers:";
	cin >> x >> y;
	cout << x << '+' << y << '=' << x + y << endl;
	cout << x << '-' << y << '=' << x - y << endl;

	cout << x << '*' << y << '=' << x * y << endl;
	float result;
	cout << x << '/' << y << '=' << (float)x / y << endl;
	//system("pause");
	return 0;
	/*enter two numbers : 4 5
		4 + 5 = 9
		4 - 5 = -1
		4 * 5 = 20
		4 / 5 = 0.8
		Press any key to continue . . .*/
}