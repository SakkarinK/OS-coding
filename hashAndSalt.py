import hashlib
import os

salt = os.urandom(32)
password = str(input('Enter password : '))
print()
key = hashlib.pbkdf2_hmac('sha256', password.encode('utf-8'), salt, 100000, dklen = 128)
print('salt :', salt)
print()
print('key :', key)
