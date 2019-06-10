import React, { Component } from 'react';

export class BadCover extends Component {
    static displayName = BadCover.name;

    constructor(props) {
        super(props);
        this.state = { badcovers: [], loading: true };

        fetch('api/cover/badcovers')
            .then(response => response.json())
            .then(data => {
                this.setState({
                    badcovers: data,
                    loading: false,
                    numCovers: data.length
                })
            });
    }

    static renderBadCoversTable(covers) {
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
            : BadCover.renderBadCoversTable(this.state.badcovers);

        return (
            <div>
                <h1>Bad-Formatted Cover: {recordCounter}</h1>
                <p>List of cover without proper naming</p>
                {contents}
            </div>
        );
    }
}
