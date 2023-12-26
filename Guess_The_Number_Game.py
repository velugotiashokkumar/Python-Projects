import random
def game():
    global a
    for i in range(a):
        print(f"You have {a} attempts remaning to guess the number!")
        num=int(input("Make a guess: "))
        if num==number:
            print(f"Your guess is right....The answer was {number}")
            exit()
        elif num>number:
            print("Your Guess is Too High")
        elif num<number:
            print("Your Guess is Too Low")
        a -= 1
        if a==0:
            print("You are out of attempts!\nBtter Luck Next Time!");exit()

print("""
   _____                       _______ _            _   _                 _               
  / ____|                     |__   __| |          | \ | |               | |              
 | |  __ _   _  ___  ___ ___     | |  | |__   ___  |  \| |_   _ _ __ ___ | |__   ___ _ __ 
 | | |_ | | | |/ _ \/ __/ __|    | |  | '_ \ / _ \ | . ` | | | | '_ ` _ \| '_ \ / _ \ '__|
 | |__| | |_| |  __/\__ \__ \    | |  | | | |  __/ | |\  | |_| | | | | | | |_) |  __/ |   
  \_____|\__,_|\___||___/___/    |_|  |_| |_|\___| |_| \_|\__,_|_| |_| |_|_.__/ \___|_|   
""")
print("Let me think a number between 1 to 50.")
number=random.randint(1,50)
i=False
while i!=True:
    level=str(input("Choose level of difficulty...Type 'easy' or 'hard': ")).lower()
    if level=='easy':
        a=10
        game()
    elif level=='hard':
        a=5
        game()
    else:
        print("Please enter valied level given above!")
        i=False
