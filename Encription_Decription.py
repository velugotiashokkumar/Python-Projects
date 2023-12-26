def encrypt(message):
    for i in range(26-shift):
        list1[i]=alphabets[i+shift]
    for i in range(shift):
        list1[-i-1]=alphabets[shift-1-i]
    for i in range(length):
        for j in range(26):
            if message[i]==alphabets[j]:
                list2[i]=list1[j]
    enc=""
    for i in range(length):
        enc=enc+list2[i]
    print(f"The encrypted message is:{enc}")

def decrypt(message):
    for i in range(26 - shift):
        list1[i] = alphabets[i + shift]
    for i in range(shift):
        list1[-i - 1] = alphabets[shift - 1 - i]
    for i in range(length):
        for j in range(26):
            if message[i]==list1[j]:
                list2[i]=alphabets[j]
    dec=""
    for i in range(length):
        dec=dec+list2[i]
    print(f"The decrypted message is:{dec}")

b='yes'
alphabets=['a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z']
while b!='no':
    a = str(input("Type 'encrypt' for encryption, type 'decrypt' for decryption:"))
    message = str(input("Type your message:"))
    length = len(message)
    #print(length)
    shift = int(input("Type the shift number:"))
    list1 = [''] * 26
    list2 = [' '] * length
    if a=="encrypt":
        encrypt(message)
    elif a=="decrypt":
        decrypt(message)
    else:
        print("Please enter the valied data!!")
        continue
    b = str(input("Type 'yes' if you want to go again.Otherwise type 'no'.\n"))
else:
    print("Good Bye")