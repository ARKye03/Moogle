import { createBoard } from '@wixc3/react-board';
import App from '../../../App';

export default createBoard({
    name: 'MainPa',
    Board: () => <App />,
    isSnippet: true,
    environmentProps: {
        canvasWidth: 177,
        canvasHeight: 270
    }
});
