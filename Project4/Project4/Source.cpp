
// Aviya levi 322286436
// Computer Sience Introduction
//Exercise#2 Question#6
// A program that receives a three digit number and calculates the amount of digits
#include <iostream>
using namespace std;
int main()
{
	int number;
	cout << "enter a three digit number:" << endl;
	cin >> number;
	int sum;
	sum = (number % 10) + (number / 10 % 10) + (number / 100);
	cout << "the sum is:" << sum << endl;
	//system("pause");
	return 0;
	/*enter a three digit number :
	145
		the sum is : 10
		Press any key to continue . . .*/
}