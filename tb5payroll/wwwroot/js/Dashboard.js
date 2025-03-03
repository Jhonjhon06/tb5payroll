// Sidebar Toggle Function
function toggleSidebar() {
    document.querySelector(".sidebar").classList.toggle("collapsed");
}


// Employee Search Function
function searchEmployee() {
    let input = document.getElementById("searchInput").value.toLowerCase();
    let rows = document.querySelectorAll("#employeeTable tbody tr");
    rows.forEach(row => {
        let text = row.textContent.toLowerCase();
        row.style.display = text.includes(input) ? "" : "none";
    });
}

