



        const register = async () => {

            console.log("************")
            console.log(document.getElementById("FirstName"));
            console.log(document.getElementById("LastName"));
            console.log(document.getElementById("UserName"));
            console.log(document.getElementById("Password"));

            let FirstName1 = document.getElementById("FirstName").value;
            let LastName1 = document.getElementById("LastName").value;
            let UserName1 = document.getElementById("UserName").value;
            let Password1 = document.getElementById("Password").value;
            if (!UserName1 || !Password1) {
                alert("username and password are required");
            }
            else { 
            const newUser = {
                FirstName: FirstName1,
                LastName: LastName1,
                Email: UserName1,
                Password: Password1,
                userId:0
            }
            try {
                const response = await fetch('api/Users/register', {
                    method: 'POST',
                    headers: {
                        "Content-type": 'application/json'
                    },
                    "body": JSON.stringify(newUser)
                })
                const responseBody = await response?.json();
                if (!responseBody) { alert("this email alaredy exits") }
                 else if (response.ok) {
                    alert("user registered successfully")
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
                alert("Error: " + error.message);
                }
            }
        }
    