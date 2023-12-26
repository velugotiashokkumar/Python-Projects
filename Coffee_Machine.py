import data
def counter():
    print("please insert coins")
    q=int(input("How many 5Rs. coins: "))
    w=int(input("How many 10Rs. coins: "))
    e=int(input("How many 20Rs. coins: "))
    cnt=(q*5)+(w*10)+(e*20)
    change=0
    if choise=='latte':
        if cnt>=data.required[0]['money']:
            change=cnt-data.required[0]['money']
            print(f"Here is your change:{change} and enjoy with {choise}")
        else:
            print("Money is not suffiicent!")
            counter()
    elif choise=='espresso':
        if cnt>=data.required[1]['money']:
            change=cnt-data.required[1]['money']
            print(f"Here is your change:{change} and enjoy with {choise}")
        else:
            print("Money is not sufficent!")
            counter()
    elif choise=='cappuccino':
        if cnt>=data.required[2]['money']:
            change=cnt-data.required[2]['money']
            print(f"Here is your change:{change} and enjoy with {choise}")
        else:
            print("Money is not sufficent!")
            counter()

w=1000
m=1000
c=1000
mny=0
choise=''
while choise!='off':
    choise=str(input("Whta would you like to have? (latte/espresso/cappuccino): ")).lower()
    if choise=='latte':
        if w>=200 and m>=50 and c>=20:
            w-=200;m-=50;c-=20;mny+=data.required[0]['money']
            counter()
        elif w<200:
            print("Sorry there is not enough water")
        elif m<50:
            print("sorry there is not enough milk")
        elif c<20:
            print("Sorry there is not enough coffee powder")
    elif choise=='espresso':
        if w>=150 and m>=100 and c>=40:
            w-=150;m-=100;c-=40;mny+=data.required[1]['money']
            counter()
        elif w<150:
            print("Sorry there is not enough water")
        elif m<100:
            print("sorry there is not enough milk")
        elif c<40:
            print("Sorry there is not enough coffee powder")
    elif choise=='cappuccino':
        if w>=100 and m>=150 and c>=50:
            w-=100;m-=150;c-=50;mny+=data.required[2]['money']
            counter()
        elif w<100:
            print("Sorry there is not enough water")
        elif m<150:
            print("sorry there is not enough milk")
        elif c<50:
            print("Sorry there is not enough coffee powder")
    elif choise=='report':
        print(f"Water:{w}ml\nMilk:{m}ml\nCoffee:{c}gm")
    elif choise=='off':
        print(f"Water:{w}ml\nMilk:{m}ml\nCoffee:{c}gm\nMoney:{mny}Rs");exit()
    else:
        print("Sorry please enter the right option!")
        continue

