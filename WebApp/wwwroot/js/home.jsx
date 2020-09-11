class CommentBox extends React.Component {
    render() {
        return React.createElement(
            'div',
            { className: 'commentBox' },
            <h1>Welcome to Contact!</h1>,
        );
    }
}

ReactDOM.render(
    React.createElement(CommentBox, null),
    document.getElementById('content'),
);

