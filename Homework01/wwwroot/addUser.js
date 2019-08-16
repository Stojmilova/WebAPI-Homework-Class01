let firstName = document.getElementById('inputFirstName');
let lastName = document.getElementById('inputLastName');
let age = document.getElementById('inputAge');

let port = "61340"

let addUser = async () => {
    let url = "http://localhost:" + port + "/api/users";
    let newUser = { firstName: firstName.value, lastName: lastName.value, age: age.value }
    await fetch(url, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(newUser)
    });
}

let addUserBtn = document.getElementById('addUser');
addUserBtn.addEventListener("click", addUser);
