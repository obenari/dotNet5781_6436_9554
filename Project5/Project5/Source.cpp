// Aviya levi 322286436
// Computer Sience Introduction
//Exercise#2 Question#6
// A program that recives the takeoff time of plane and then the flight time of the plane and prints the landding time
#include <iostream>
using namespace std;
int main()
{
	int hour;
	int minut;
	int secoend;
	int x;
	int y;
	int z;
	int h1;
	int h2;
	int h3;
	cout << "enter flight takeoff:" << endl;
	cin >> hour >> minut >> secoend;
	cout << "enter flight duration:";
	cin >> x >> y >> z;
	h1 = (secoend + z) % 60;
	h2 = (secoend + z) / 60 + (minut + y) % 60;
	h3 = (hour + x) % 24 + (minut + y) / 60;
	cout << "flight arrival is:";
	cout << h3 << ':' << h2 << ':' << h1 << endl;
	/*system("pause");*/
	return 0;
	/*enter flight takeoff :
	45 10 10
		enter flight duration : 20 30 25
		flight arrival is : 17 : 40 : 35
		Press any key to continue . . .*/
}