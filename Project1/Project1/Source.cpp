//Aviya levi 322286436
//Computer Science Introduction
//Exercise 2 Question 4
//A program tat absorbs two variables and calculates the third variable according to a specific formula
#include <iostream>
using namespace std;
int main()
{
	int a;
	int b;
	float c;
	cout << "enter two numbers:";
	cin >> a >> b;
	c = (float)(5 * a + 3) / (6 * b + 2);
	cout << "c=" << c << endl;
	/*system("pause");*/
	return 0;
	//enter two numbers : 6 5
	//	c = 1.03125
	//	Press any key to continue . . .


}