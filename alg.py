import os
import json
import hashlib
import numpy as np


def vector_norm(vect):
    lg_vec = np.linalg.norm(vect)
    return vect / lg_vec if lg_vec != 0 else 0

def cosine_similarity(norm_vec_main, norm_vec_side):
    dot_product = np.dot(norm_vec_main, norm_vec_side) 
    lg_main = np.linalg.norm(norm_vec_main) 
    lg_side = np.linalg.norm(norm_vec_side)
    return dot_product / (lg_main * lg_side) if lg_main != 0 and lg_side != 0 else 0

def max_euclidian_distance(vectors):
    min_vector = np.min(vectors, axis=0)
    max_vector = np.max(vectors, axis=0)
    return np.linalg.norm(np.array(max_vector) - np.array(min_vector))

def combine_similarity(vec1, vec2, max_euc):
    cos_sim = cosine_similarity(vec1, vec2)
    euc_dist = np.linalg.norm(np.array(vec1) - np.array(vec2))
    return cos_sim - 1/2 * euc_dist / max_euc

def initializate_json(filename):
    if not os.path.exists(filename):
        with open(filename, 'w') as file:
            json.dump([], file, indent=6)

def hash_password(password):
    return hashlib.sha256(password.encode('utf-8')).hexdigest()

def add_user(filename, username, name, surname, password, inform_vector, inverse_vector):
    with open(filename, 'r') as file:
        users = json.load(file)
    user_id = len(users) + 1
    new_user = {
        "id": user_id,
        "username": username,
        "name": name,
        "surname": surname,
        "password": password,
        "info_vector": inform_vector,
        "inverse_vector": inverse_vector
    }

    users.append(new_user)

    with open(filename, 'w') as file:
        json. dump(users, file, indent = 6)

def user_validation(filename, username, name, surname, password, inform_vector, inverse_vector):
    with open(filename, 'r') as file:
        users = json.load(file)
    if any(user["username"] == username for user in users):
        return "Ошибка! Такой логи уже существует!"
    add_user(filename, username, name, surname, password, inform_vector, inverse_vector)

def create_users(filename): 
    users = [
        {
            "id": 1,
            "username": "user1",
            "name": "Alice",
            "surname": "Smith",
            "password": "0b14d501a594442a01c6859541bcb3e8164d183d32937b851835442f69d5c94e",
            "info_vector": [
                1, 0.5, 0.25, 0.75, 0, 1, 0.25, 0.75, 0.5, 0, 0.75, 0.25, 1
            ],
            "inverse_vector": [1]
        },
        {
            "id": 2,
            "username": "user2",
            "name": "Bob",
            "surname": "Johnson",
            "password": "6cf615d5bcaac778352a8f1f3360d23f02f34ec182e259897fd6ce485d7870d4",
            "info_vector": [
                0.5, 1, 0.75, 0, 1, 0.25, 0.25, 0.5, 1, 0.75, 0.5, 0.75, 0.25
            ],
            "inverse_vector": [1]
        },
        {
            "id": 3,
            "username": "user3",
            "name": "Charlie",
            "surname": "Brown",
            "password": "5906ac361a137e2d286465cd6588ebb5ac3f5ae955001100bc41577c3d751764",
            "info_vector": [
                1, 0.75, 0.5, 0.25, 0, 1, 0, 0.5, 0.25, 1, 0.75, 0.25, 0.75
            ],
            "inverse_vector": [1]
        },
        {
            "id": 4,
            "username": "user4",
            "name": "Diana",
            "surname": "Miller",
            "password": "b97873a40f73abedd8d685a7cd5e5f85e4a9cfb83eac26886640a0813850122b",
            "info_vector": [
                0.25, 0.5, 1, 0, 0.75, 0.75, 0.25, 1, 0, 0.5, 0.25, 0.5, 0.5
            ],
            "inverse_vector": [0]
        },
        {
            "id": 5,
            "username": "user5",
            "name": "Eve",
            "surname": "Garcia",
            "password": "8b2c86ea9cf2ea4eb517fd1e06b74f399e7fec0fef92e3b482a6cf2e2b092023",
            "info_vector": [
                0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5
            ],
            "inverse_vector": [1]
        },
        {
            "id": 6,
            "username": "user6",
            "name": "Frank",
            "surname": "Wilson",
            "password": "598a1a400c1dfdf36974e69d7e1bc98593f2e15015eed8e9b7e47a83b31693d5",
            "info_vector": [
                0.25, 0.75, 1, 0.25, 0.5, 0, 1, 0.25, 0.75, 0, 0.25, 0.75, 0.5
            ],
            "inverse_vector": [0]
        },
        {
            "id": 7,
            "username": "user7",
            "name": "Grace",
            "surname": "Moore",
            "password": "5860836e8f13fc9837539a597d4086bfc0299e54ad92148d54538b5c3feefb7c",
            "info_vector": [
                1, 0.5, 0.25, 0, 0.25, 0.75, 0.5, 0.75, 0, 0.25, 0.75, 0.5, 0.25
            ],
            "inverse_vector": [0]
        },
        {
            "id": 8,
            "username": "user8",
            "name": "Henry",
            "surname": "Taylor",
            "password": "57f3ebab63f156fd8f776ba645a55d96360a15eeffc8b0e4afe4c05fa88219aa",
            "info_vector": [
                0.75, 0.25, 0.5, 0.25, 1, 0, 0.75, 0.5, 0.25, 0.5, 0.25, 0.5, 1
            ],
            "inverse_vector": [0]
        }
    ]
    for user in users:
        user_validation(filename, user['username'], user['name'], user['surname'], user['password'], user['info_vector'], user['inverse_vector'])

initializate_json("users.json")
initializate_json("main_user.json")
initializate_json("answers.json")
initializate_json("anti_answers.json")
create_users("users.json")

main_user_inform_vector = np.array([1,1,1,1,1,1,1,1,1,1,1,1,1])
us_json_vector = main_user_inform_vector.tolist()
user_validation("main_user.json", "use11", "Stas", "Shustov", hash_password("password11"), us_json_vector, 0)

with open("users.json", 'r') as file:
    users = json.load(file)
    users_arr = []
    for user in users:
        users_arr.append(user["info_vector"])
    max_euclidian_dist = max_euclidian_distance(users_arr)

similarity_probability = []

with open ('main_user.json', 'r') as file1:
    main_user = json.load(file1)
    with open("users.json", 'r') as file2:
        users = json.load(file2)
        for user in users:
            similarity_probability.append(
                [user["id"],
                float(combine_similarity(main_user[0]["info_vector"],
                user["info_vector"],
                max_euclidian_dist))],
           )
            
similarity_probability.sort(key = lambda x: x[1], reverse= True)

print(similarity_probability[-3:])
#пройдемся по user'ам с целью поиска тех, кто подходит наиболее и наименее
with open("users.json", 'r') as file:
    users = json.load(file)
    for user in users:
        for i in range(3):
            if user["id"] == similarity_probability[i][0]:
                add_user("answers.json", user["username"], user["name"], user["surname"], user["password"], user["info_vector"], user["inverse_vector"])
        for i in range(len(similarity_probability) -3, len(similarity_probability)):
            if user["id"] == similarity_probability[i][0]:
                add_user("anti_answers.json", user["username"], user["name"], user["surname"], user["password"], user["info_vector"], user["inverse_vector"])