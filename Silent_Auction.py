import os
i=True
list1=[]
while i!=False:
    name = str(input("Enter your name?:"))
    bid = int(input("Enter your Bid?:"))
    dict1 = {}
    dict1["Name"]=name
    dict1["bid"]=bid
    list1.append(dict1)
    o=str(input("Are there any other bidder if yes type 'yes' if no type 'no':"))
    if o=='yes':
        i=True
    elif o=='no':
        i=False
    os.system('cls')
l=len(list1)
maximum=list1[0]["bid"]
for i in range(1,l):
    if list1[i]["bid"] > maximum :
        maximum=list1[i]["bid"]
        n=list1[i]["Name"]
print(f"the maximum bitter is {n} and bitted {maximum}")