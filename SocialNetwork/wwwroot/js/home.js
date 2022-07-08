document.addEventListener("DOMContentLoaded", () => {
    const btnPost = document.getElementById("btn-post");
    const textArea = document.getElementById("publicationContext");
    const commentTextAreas = document.getElementsByClassName("commentTextArea");
    const commentBtnGroup = document.getElementsByClassName("btnCommentHome");

    textArea.oninput = () => {
        if (textArea.value) {
            btnPost.disabled = false;
        } else {
            btnPost.disabled = true;
        }
    };

    for (let i = 0; i < commentTextAreas.length; i++) {
        const textArea = commentTextAreas[i];
        const commentBtn = commentBtnGroup[i];
        textArea.oninput = () => {
            if (textArea.value) {
                commentBtn.disabled = false;
            } else {
                commentBtn.disabled = true;
            }
        };
    }
})