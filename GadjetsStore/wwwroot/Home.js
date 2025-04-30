


const login = async () => {
    alert("rger")
    console.log("************")
    let userName = document.getElementById("UserName").value;
    let password = document.getElementById("Password").value;
    if (!UserName || !Password) {
        alert("username and password are required");
    }  
    const userForLogin = {       
      Password: password,
       UserName: userName
    }
    try {
        const response = await fetch(`api/Users/login?UserName=${userForLogin.UserName}&Password=${userForLogin.Password}`, {
            method: 'POST',
            headers: {
                "Content-type": 'application/json'
            },
            query: {
                UserName: userForLogin.UserName,
                Password: userForLogin.Password
            }
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
