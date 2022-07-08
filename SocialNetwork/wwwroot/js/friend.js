document.addEventListener("DOMContentLoaded", () => {
    const commentTextAreas = document.getElementsByClassName("commentTextArea");
    const commentBtnGroup = document.getElementsByClassName("btnCommentFriend");
    console.log(commentBtnGroup);

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
});