import React, { Component } from 'react';

export class Cover extends Component {
    static displayName = Cover.name;

    constructor(props) {
        super(props);
        this.state = { covers: [], loading: true };

        fetch('api/cover/covers')
            .then(response => response.json())
            .then(data => {
                this.setState({ covers: data, loading: false });
            });
    }

    static renderCoversTable(covers) {
        return (
            <table className='table table-striped'>
                <thead>
                    <tr>
                        <th>Series</th>
                        <th>Cast</th>
                        <th>Release Date</th>
                        <th>Filename</th>
                    </tr>
                </thead>
                <tbody>
                    {covers.map(cover =>
                        <tr key={cover.id}>
                            <td>{cover.series}</td>
                            <td>{cover.cast}</td>
                            <td>{cover.releasedate}</td>
                            <td>{cover.filename}</td>
                        </tr>
                    )}
                </tbody>
            </table>
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : Cover.renderCoversTable(this.state.covers);

        return (
            <div>
                <h1>All Covers</h1>
                <p>List of all covers</p>
                {contents}
            </div>
        );
    }
}
