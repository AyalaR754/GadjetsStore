


const login = async () => {
    alert("rger")
 
    let email = document.getElementById("UserName").value;
    let password = document.getElementById("Password").value;
    if (!email || !Password) {
        alert("email and password are required");
    }  
    const userForLogin = {       
      Password: password,
        Email: email
    }
    try {

        const response = await fetch(`api/Users/login`, {
            method: 'POST',
            headers: {
                "Content-type": 'application/json'
            },
            "body": JSON.stringify(userForLogin)
        });

        if (response.ok) {
            const user = await response.json();
            if (user) {
                jsonUser = JSON.stringify(user);
                localStorage.setItem("user", jsonUser);

                window.location.href='Update.html'
            }
            else { alert("user registered not successfully")}
            console.log("ppppppp");
        }
        else {
            switch (response.status) {
                case 400:
                    const badRequestData = await response.json();
                    alert(`Bad request: ${badRequestData.message || 'Invalid input. Please check your data.'}`);
                    break;
                case 401:
                    alert("Unauthorized: Please check your credentials.");
                    break;
                case 500:
                    alert("Server error. Please try again later.");
                    break;
                default:
                    alert(`Unexpected error: ${response.status}`);
            }
     }   
    }
    catch (error) {
        console.log("opoo")
        alert(error)
    }
}
