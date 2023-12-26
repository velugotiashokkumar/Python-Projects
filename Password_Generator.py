import random
char=['q','w','e','r','t','y','u','i','o','p','a','s','d','f','g','h','j','k','l','z','x','c','v','b','n','m','Q','W','E','R','T','Y','U','I','O','P','A','S','D','F','G','H','J','K','L','Z','X','C','V','B','N','M']
integer=['0','1','2','3','4','5','6','7','8','9']
special=['~','`','!','@','#','$','%','^',"&","*",'(',')','_','-','+','=','|','/','?']
password=''
a=int(input("Enter how many characters you want :"))
b=int(input("Enter how many integers you want :"))
c=int(input("Enter hoe many special characters you want :"))
for i in range(a):
    d=random.choice(char)
    password=password+d
for i in range(b):
    e=random.choice(integer)
    password=password+e
for i in range(c):
    f=random.choice(special)
    password=password+f
print(password)
list1=list(password)
random.shuffle(list1)
l=len(list1)
passcode=''
for i in range(l):
    g=list1[i]
    passcode=passcode+g
print(passcode)