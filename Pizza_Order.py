a=int(input("Small Pizza-1\nMedium Pizza-2\nLarge Pizza-3\nOrder Please : "))
p=0
if a==1:
    p=p+100
    b=int(input("Want Pepperoni?\nyes-1\nno-2\n: "))
    if b==1:
        p=p+30
    elif b==2:
        p=p
elif a==2:
    p=p+200
    b=int(input("Want Pepperoni?\nyes-1\nno-2\n: "))
    if b==1:
        p=p+50
    elif b==2:
        p=p
elif a==3:
    p=p+300
    b=int(input("Want Pepperoni?\nyes-1\nno-2\n: "))
    if b==1:
        p=p + 50
    elif b==2:
        p=p
q=0
c=int(input("Want extra cheese?\nyes-1\nno-2\n: "))
if c==1:
    q=q+20
elif c==2:
    q=q
T=p+q
print(f"TOTAL PRICE : Rs{T}/-")
