window.quillEditors = {};

window.quillEditor = {
    create: function (editorId, initialHtml) {
        const editorElement = document.getElementById(editorId);

        if (!editorElement) {
            return;
        }

        if (window.quillEditors[editorId]) {
            return;
        }

        const quill = new Quill(editorElement, {
            theme: "snow",
            modules: {
                toolbar: [
                    [{ header: [1, 2, 3, false] }],
                    ["bold", "italic", "underline"],
                    [{ list: "ordered" }, { list: "bullet" }],
                    ["link"],
                    ["clean"]
                ]
            }
        });

        if (initialHtml) {
            quill.root.innerHTML = initialHtml;
        }

        window.quillEditors[editorId] = quill;
    },

    getHtml: function (editorId) {
        const quill = window.quillEditors[editorId];

        if (!quill) {
            return "";
        }

        return quill.root.innerHTML;
    },

    insertImage: function (editorId, imageUrl) {
        const quill = window.quillEditors[editorId];

        if (!quill) {
            return;
        }

        const range = quill.getSelection(true);
        quill.insertEmbed(range.index, "image", imageUrl);
    }
};