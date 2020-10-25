//Aviya levi 322286436
//Computer Since Introduction
// Exercise #2 Question# 5
// A program that absorbs two variables and replaces the values of the variables 
#include <iostream>
using namespace std;
int main()
{
	int x;
	int y;
	cout << "enter two nambers:";
	cin >> x >> y;
	cout << "x=" << x << endl << "y=" << y << endl;
	int x1;
	x1 = x;
	x = y;
	y = x1;
	cout << "x=" << x << endl << "y=" << y << endl;
	/*system("pause");*/
	return 0;
	/*enter two nambers : 6 7
		x = 6
		y = 7
		x = 7
		y = 6
		Press any key to continue . . .*/
}