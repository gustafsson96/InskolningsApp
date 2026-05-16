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
    },

    // Insert YouTube iframe into editor
    insertYoutube: function (editorId, url) {
        const quill = window.quillEditors[editorId];

        if (!quill) {
            return;
        }

        // Extract video id from YouTube url
        const regex = /(?:youtube\.com\/watch\?v=|youtu\.be\/)([^&]+)/;
        const match = url.match(regex);

        // Stop if url is invalid
        if (!match) {
            alert("Ogiltig YouTube-länk.");
            return;
        }

        const videoId = match[1];

        // Responsive YouTube iframe
        const iframeHtml = `
            <div class="video-embed">
                <iframe
                    src="https://www.youtube.com/embed/${videoId}"
                    title="YouTube video"
                    frameborder="0"
                    allowfullscreen>
                </iframe>
            </div>
        `;

        // Insert iframe at current cursor position
        const range = quill.getSelection(true);

        quill.clipboard.dangerouslyPasteHTML(
            range.index,
            iframeHtml
        );
    }
};