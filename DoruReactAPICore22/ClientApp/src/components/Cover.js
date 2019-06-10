import React, { Component } from 'react';

export class Cover extends Component {
    static displayName = Cover.name;

    constructor(props) {
        super(props);
        this.state = { covers: [], loading: true };

        fetch('api/cover/covers')
            .then(response => response.json())
            .then(data => {
                this.setState({
                    covers: data,
                    loading: false,
                    numCovers: data.length
                });
            });
    }

    static renderCoversTable(covers) {
        return (
            <table className='table table-striped' style={{ width: 1200 }}>
                <thead>
                    <tr>
                        <th>Series</th>
                        <th>Cast</th>
                        <th>Release Date</th>
                        <th>Filename</th>
                        <th>Label</th>
                        <th>Id</th>
                    </tr>
                </thead>
                <tbody>
                    {covers.map(cover =>
                        <tr key={cover.id}>
                            <td>{cover.series}</td>
                            <td>{cover.cast}</td>
                            <td>{cover.releasedate}</td>
                            <td>{cover.filename}</td>
                            <td>{cover.label}</td>
                            <td>{cover.id}</td>
                        </tr>
                    )}
                </tbody>
            </table>
        );
    }

    render() {
        let recordCounter = "";
        if (!this.state.loaded) {
            recordCounter = " Record(s)";
        } if (this.state.numRows < 1) {
            recordCounter = "No" + recordCounter;
        } else {
            recordCounter = this.state.numCovers + recordCounter;
        };

        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : Cover.renderCoversTable(this.state.covers);

        return (
            <div>
                <h1>All Covers: {recordCounter}</h1>
                <p>List of all covers</p>
                {contents}
            </div>
        );
    }

}

