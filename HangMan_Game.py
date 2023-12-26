import random
import data

list1=random.choice(data.names)
l=len(list1)
#print(list1,l)
list2=['_']*(len(list1))
print("let's play Hangman!!")
life=0
print("You have only 6 lifes so try to guess the word within 6 attemps! Good Luck !!")
print(list2)
while life!=6:
    a=''
    for i in list2:
        a+=i
    if a==list1:
        print("You won the game!!");exit()
    else:
        letter = str(input("Guess a letter: ")).lower()
        if letter in list1:
            for i in range(l):
                if letter==list1[i]:
                    list2[i]=letter
            print(list2,data.hangman[life])
        else:
            print(f"You guessed '{letter}' that is not present in the word. So you lose a life")
            print(f"{list2}\n{data.hangman[life+1]}")
            life += 1
            if life==6:
                print(f"You Lose the Game!!\nThe word is {list1}")