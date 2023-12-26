import os
def add(a,b):
    c=a+b
    return c
def sub(a,b):
    c=a-b
    return c
def mul(a,b):
    c=a*b
    return c
def div(a,b):
    c=a/b
    return c
def calucator():
    a=int(input("Enter first number:"))
    print("+\n-\n*\n/")
    choise='y'
    while choise=='y' :
        o=str(input("Pick an operator:"))
        b=int(input("Enter next number:"))
        if o=='+':
            print(f"{a}+{b}={add(a,b)}")
            d=add(a,b)
        elif o=='-':
            print(f"{a}-{b}={sub(a,b)}")
            d=sub(a,b)
        elif o=='*':
            print(f"{a}*{b}={mul(a,b)}")
            d=mul(a,b)
        elif o=='/':
            print(f"{a}/{b}={div(a,b)}")
            d=div(a,b)
        else:
            print("please enter the valied operator!")
            calucator()
        choise=str(input(f"enter 'y' to continue calucation with {d} or 'n' to start new calucation or 'x' to exit")).lower()
        if choise=='n':
            os.system('cls')
            calucator()
        elif choise=='y':
            a=d
            continue
        elif choise=='x':
            print("bye")
            exit()
calucator()