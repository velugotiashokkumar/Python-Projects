import random
import data
count=0
def game():
    global count
    dict1=random.choice(data.data)
    dict2 = random.choice(data.data)
    if dict1['name'] == dict2['name']:
        game()
    else:
        print(data.Higher)
        print(f"Compare1: {dict1['name']}, a {dict1['description']}, from {dict1['country']}")
        print(data.vs)
        print(f"Compare2: {dict2['name']}, a {dict2['description']}, from {dict2['country']}")
        a=int(input("Who has more followers? Type '1' or '2': "))
        choise=True;
        while choise!=False:
            if a==1:
                if dict1['followers']>dict2['followers']:
                   count+=1
                   print(f"Your are right. Your score is {count}")
                   game()
                else:
                    print(data.Higher)
                    print(f"Your are Wrong.. Your final score is {count}")
                    Choise=False;exit()
            elif a==2:
                if dict2['followers']>dict1['followers']:
                   count+=1
                   print(f"Your are right. Your score is {count}")
                   game()
                else:
                    print(data.Higher)
                    print(f"Your are Wrong.. Your final score is {count}")
                    choise=False;exit()
            else:
                print("Please Enter a Valied Number!")
                game()
game()