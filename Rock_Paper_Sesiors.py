u=int(input("0-Rock\n1-Scissors\n2-Paper\nEnter your choise : "))
import random
c=random.randint(0,2)
if 0<=u<=2 :
    if c==u :
        print("Draw.\nBetter luck next time!")
    elif u==0 and c==2 :
        print("You Won!")
    elif u==2 and c==0 :
        print("You Lose")
    elif c>u :
        print("You Lose")
    elif u>c :
        print("You Won!")
else:
    print("Please enter the valied number.")
